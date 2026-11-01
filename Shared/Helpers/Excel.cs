namespace Wks.Mains;

///
/// Summary:
///     Provides Excel file operations including reading, writing, and batch processing.
///     Supports large file handling with memory-efficient streaming and batch operations.
///
public class Excel
{
    ///
    /// Summary:
    ///     Maximum number of rows to read when in preview mode.
    ///     Used to limit data processing for preview purposes.
    ///
    private const int INITIAL_BATCH = 0;
    private const int INITIAL_ROW = 0;

    private const int PATH_SIZE_PREVIEW = 5;

    ///
    /// Summary:
    ///     Maximum number of rows to process in each batch from environment variable.
    ///     Used for controlling memory usage during large Excel file processing.
    ///
    private const int PATH_SIZE_DEFAULT = 2000;

    ///
    /// Summary:
    ///     Reads an Excel file from a memory stream and processes data in batches.
    ///     Supports both full processing and preview modes with configurable batch sizes.
    ///
    /// Parameters:
    ///     stream:
    ///         The memory stream containing the Excel file data.
    ///     action:
    ///         The async action to perform on each batch of records and schema.
    ///     isPreview:
    ///         If true, limits processing to preview size for quick data inspection.
    ///     size:
    ///         Maximum number of rows per batch.
    ///     slugify:
    ///         If true, slugifies header names and record dictionary keys so callers
    ///         do not need to map column keys manually.
    ///
    /// Returns:
    ///     A list of column names (schema) extracted from the first row of the Excel file.
    ///
    public static async Task<List<string>> ReadAsync(
        MemoryStream stream,
        Func<List<object>, Extend, Task> action,
        bool isPreview = false,
        int size = PATH_SIZE_DEFAULT,
        bool slugify = false
    )
    {
        ///
        /// Summary:
        ///     Registers the code pages encoding provider for Excel file reading.
        ///
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

        int batch = INITIAL_BATCH;
        int indexRow = INITIAL_ROW;
        List<string> columns = [];
        List<object> records = [];

        do
        ///
        /// Summary:
        ///     Loops through each row in the current Excel sheet.
        ///     Extracts schema from the first row and records from subsequent rows.
        ///
        while (reader.Read())
        {
            ///
            /// Summary:
            ///     Breaks processing if in preview mode and maximum preview rows reached.
            ///
            if (isPreview is true && indexRow > PATH_SIZE_PREVIEW)
                break;

            Dictionary<string, string> record = [];
            for (
                int indexColumn = INITIAL_ROW;
                indexColumn < reader.FieldCount;
                indexColumn++
            )
            {
                object value = reader.GetValue(indexColumn);

                ///
                /// Summary:
                ///     First row is treated as schema (column headers).
                ///     Optionally slugifies column names for consistent key mapping.
                ///
                if (indexRow is INITIAL_ROW)
                {
                    string column = Format.Default(
                        Convert.ToString(value),
                        string.Empty
                    );
                    columns.Add(
                        slugify ? Format.ToSlugify(column).ToLower() : column
                    );
                }
                ///
                /// Summary:
                ///     Subsequent rows contain data values.
                ///     Maps values to schema columns using dictionary.
                ///
                else if (value.Justifiable())
                    record.TryAdd(
                        columns[indexColumn],
                        Convert.ToString(value)
                    );
            }

            indexRow++;
            if (record.Count is not INITIAL_ROW)
                records.Add(record);

            ///
            /// Summary:
            ///     Continues reading if batch size not reached yet.
            ///
            if (records.Count < size)
                continue;

            await action(records, new(columns) { Batch = batch++ });
            records.Clear();
        }
        ///
        /// Summary:
        ///     Loops through all sheets in the Excel file.
        ///
        while (reader.NextResult());

        ///
        /// Summary:
        ///     Processes any remaining records that didn't fill a complete batch.
        ///
        if (records.Count > INITIAL_ROW)
            await action(records, new(columns) { Batch = batch });

        return columns;
    }

    private static IEnumerable<object> Selector(
        MemoryStream memoryStream,
        List<string> columns,
        Func<IDictionary<string, object>, Extend, object> selector
    )
    {
        memoryStream.Position = INITIAL_ROW;
        int index = INITIAL_ROW;
        foreach (
            IDictionary<string, object> record in MiniExcelLibs
                .MiniExcel.Query(memoryStream, useHeaderRow: true)
                .Select(record => (IDictionary<string, object>)record)
        )
        {
            yield return selector(record, new(columns) { Index = index });
            index++;
        }
    }

    ///
    /// Summary:
    ///     Rewrites the Excel stream by applying a selector to each row and saving
    ///     the result into a fresh memory stream.
    ///
    /// Parameters:
    ///     memoryStream:
    ///         The input Excel stream to process.
    ///
    ///     selector:
    ///         Selector function for transforming each row dictionary.
    ///
    /// Returns:
    ///     A MemoryStream containing the rewritten Excel worksheet.
    ///
    public static async Task<MemoryStream> ReWriteAsync(
        MemoryStream memoryStream,
        Func<IDictionary<string, object>, Extend, object> selector
    )
    {
        byte[] bytes = memoryStream.ToArray();

        MemoryStream stream = new();
        await MiniExcelLibs.MiniExcel.SaveAsAsync(
            stream,
            Selector(
                new(bytes),
                MiniExcelLibs
                    .MiniExcel.GetColumns(
                        new MemoryStream(bytes),
                        useHeaderRow: true
                    )
                    .ToList(),
                selector
            )
        );

        stream.Position = INITIAL_ROW;
        return stream;
    }

    ///
    /// Summary:
    ///     Writes a collection of dictionary records into a new Excel workbook.
    ///     Each dictionary becomes an Excel row with columns mapped by keys.
    ///
    /// Parameters:
    ///     records:
    ///         A list of row dictionaries where each key is a column name and each
    ///         value is the cell text to write for that row.
    ///
    /// Returns:
    ///     A MemoryStream containing the generated Excel file ready for download
    ///     or further processing.
    ///
    public static async Task<MemoryStream> WriteAsync(
        List<Dictionary<string, string>> records
    )
    {
        MemoryStream stream = new();
        await MiniExcelLibs.MiniExcel.SaveAsAsync(stream, records);

        stream.Position = INITIAL_ROW;
        return stream;
    }

    ///
    /// Summary:
    ///     Counts the total number of data rows in an Excel file.
    ///     Excludes the header row from the count.
    ///
    /// Parameters:
    ///     stream:
    ///         The memory stream containing the Excel file data.
    ///
    /// Returns:
    ///     The total number of data rows in the Excel file.
    ///
    public static int Count(MemoryStream stream) =>
        MiniExcelLibs.MiniExcel.Query(stream, useHeaderRow: true).Count();

    #region Object

    public class Extend(List<string> columns)
    {
        public List<string> Columns { get; set; } = columns;
        public int Index { get; set; } = 0;
        public int Batch { get; set; } = 0;
    }
    #endregion
}
