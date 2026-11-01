namespace Shared;

///
/// Summary:
///     Defines the default protocol for public domain URLs.
///
public class UrlConstant
{
    public const string DEFAULT_PROTOCOL = "https://";
}

///
/// Summary:
///     Defines regex patterns for input validation.
///     CUSTOM_LINK_TOKEN allows alphanumeric characters, hyphens, and underscores only.
///
public class RegexConstant
{
    public const string CUSTOM_LINK_TOKEN = @"^[a-zA-Z0-9_\-]+$";
}

///
/// Summary:
///     Defines file extension and MIME type constants used throughout the application.
///     EXCEL is the spreadsheet document extension. OCTEL_STREAM is the MIME type for binary downloads.
///     APPLICATION_JSON is the MIME type for JSON request and response bodies.
///
public class Extension
{
    public const string EXCEL = ".xlsx";
    public const string OCTEL_STREAM = "application/octet-stream";
    public const string APPLICATION_JSON = "application/json";
}

///
/// Summary:
///     Defines comparison operator symbols for broadcast filter conditions.
///     Get returns symbols mapped by BroadcastConditionNameEnum index for From data sources:
///     equals, not equals, greater than, less than.
///
public class OperatorConstant
{
    public static List<string> Get() =>
        ["==", "!=", ">", "<", "IN", "NOT IN", "LIKE"];
}

///
/// Summary:
///     Defines default file names for user profile images.
///     PROFILE_AVATAR and PROFILE_COVER are the default avatar and cover file names.
///
public class FileConstant
{
    public const string PROFILE_AVATAR = "Avatar.JPG";
    public const string PROFILE_COVER = "Cover.JPG";
}

///
/// Summary:
///     Contains Vietnamese AI prompt templates for generating descriptions.
///     FINANCIAL_TRANSACTION_DESCRIPTION writes a spend description from name and amount.
///     REDIRECT_SHORT_LINK_DESCRIPTION, REDIRECT_CAMPAIGN_DESCRIPTION,
///     REDIRECT_TEMPLATE_URL_DESCRIPTION, and REDIRECT_WEBHOOK_DESCRIPTION cover
///     short links, campaigns, bulk URL templates, and webhook integrations.
///
public class PromptConstant
{
    public const string FINANCIAL_TRANSACTION_DESCRIPTION =
        $@"Ngữ cảnh: Ứng dụng chi tiêu cá nhân.
        Giao dịch: 'input:0'
        Số tiền: 'input:1'
        Yêu cầu:
        - Viết mô tả 3-4 câu tối thiểu 20 chữ về giao dịch trên
        - Tiếng Việt
        - Trả về 1 dòng chữ duy nhất
        - Không giải thích";

    public const string REDIRECT_SHORT_LINK_DESCRIPTION =
        $@"Ngữ cảnh: Ứng dụng quản lý liên kết rút gọn.
        Giao dịch: 'input:0'
        Mô tả thêm: 'input:1'
        Yêu cầu:
        - Viết mô tả 3-4 câu tối thiểu 20 chữ về việc tạo một liên kết rút gọn
        - Nội dung xoay quanh mục đích sử dụng và lợi ích của link
        - Tiếng Việt
        - Trả về 1 dòng chữ duy nhất
        - Không giải thích";

    public const string REDIRECT_CAMPAIGN_DESCRIPTION =
        $@"Ngữ cảnh: Ứng dụng quản lý chiến dịch liên kết rút gọn.
        Chiến dịch: 'input:0'
        Mục tiêu: 'input:1'
        Yêu cầu:
        - Viết mô tả 3-4 câu tối thiểu 20 chữ về chiến dịch sử dụng các liên kết rút gọn
        - Nội dung liên quan đến marketing, theo dõi hiệu quả hoặc phân phối link
        - Tiếng Việt
        - Trả về 1 dòng chữ duy nhất
        - Không giải thích";

    public const string REDIRECT_TEMPLATE_URL_DESCRIPTION =
        $@"Ngữ cảnh: Ứng dụng tạo liên kết rút gọn hàng loạt.
        Nguồn dữ liệu: 'input:0'
        Mục đích: 'input:1'
        Yêu cầu:
        - Viết mô tả 3-4 câu tối thiểu 20 chữ về việc tạo nhiều liên kết rút gọn cùng lúc
        - Nội dung xoay quanh hiệu suất, tự động hóa và tiết kiệm thời gian
        - Tiếng Việt
        - Trả về 1 dòng chữ duy nhất
        - Không giải thích";

    public const string REDIRECT_WEBHOOK_DESCRIPTION =
        $@"Ngữ cảnh: Ứng dụng tạo liên kết rút gọn hàng loạt.
        Nguồn dữ liệu: 'input:0'
        Sự kiện: 'input:1'
        Yêu cầu:
        - Viết mô tả 3-4 câu tối thiểu 20 chữ về việc tạo webhook tích hợp cho các hệ thống bên ngoài với sự kiện
        - Nội dung xoay quanh hiệu suất, tự động hóa và tiết kiệm thời gian
        - Tiếng Việt
        - Trả về 1 dòng chữ duy nhất
        - Không giải thích";
}

///
/// Summary:
///     Contains miscellaneous application constants including hosts, media types, and default filters.
///     LOCALHOST, LOOPBACK_IPV4, and BIND_ALL are network bind addresses.
///     GROUP_IMAGE is the default group image identifier.
///     TYPE_VIDEO, TYPE_MUSIC, and TYPE_PICTURE list supported media extensions.
///     DATA_DEFAULT_FILTER_NAME and DATA_DEFAULT_FILTER_DESCRIPTION initialize the default data filter.
///
public class Constant
{
    public const string LOCALHOST = "localhost";
    public const string LOOPBACK_IPV4 = "127.0.0.1";
    public const string BIND_ALL = "0.0.0.0";

    public const string GROUP_IMAGE =
        "93dea0ec-02ac-4b56-8b98-720f59eb47cc.jpg";

    public static readonly List<string> TYPE_VIDEO =
    [
        ".mp4",
        ".3gp",
        ".avi",
        ".mov",
    ];

    public static readonly List<string> TYPE_MUSIC = [".mp3"];

    public static readonly List<string> TYPE_PICTURE =
    [
        ".png",
        ".jpg",
        ".jpeg",
    ];

    public const string DATA_DEFAULT_FILTER_NAME = "Mặc định";

    public const string DATA_DEFAULT_FILTER_DESCRIPTION =
        "Bộ lọc mặc định được khởi tạo theo dữ liệu";
}

///
/// Summary:
///     Defines global prefixes for generated business codes and document numbers.
///     PRODUCT is SP for catalog product code. SKU is SK for warehouse sku code. ORDER is ORD for shop orders.
///     Logistics documents: GOODS_RECEIPT GR, GOODS_ISSUE GI, INTERNAL_TRANSFER IT,
///     WAREHOUSE_TRANSFER WT, STOCK_COUNT SC, INVENTORY_ADJUSTMENT ADJ,
///     INVENTORY_TRANSACTION INV for inventory ledger entries.
///     Billing documents: INVOICE INV, PAYMENT PAY, REFUND REF.
///
public class PrefixConstant
{
    public const string PRODUCT = "SP";
    public const string SKU = "SK";
    public const string ORDER = "ORD";
    public const string GOODS_RECEIPT = "GR";
    public const string GOODS_ISSUE = "GI";
    public const string INTERNAL_TRANSFER = "IT";
    public const string WAREHOUSE_TRANSFER = "WT";
    public const string STOCK_COUNT = "SC";
    public const string INVENTORY_ADJUSTMENT = "ADJ";
    public const string INVENTORY_TRANSACTION = "INV";
    public const string INVOICE = "INV";
    public const string PAYMENT = "PAY";
    public const string REFUND = "REF";
}

///
/// Summary:
///     Defines billing constants that are not document-number prefixes.
///     MANUAL_PROVIDER is the provider name for manual cashier confirmation.
///
public class BillingDocumentConstant
{
    public const string MANUAL_PROVIDER = "MANUAL";
}

///
/// Summary:
///     Defines audit action and entity-name constants for billing operations.
///     Actions cover invoice, payment, refund creation, and webhook events.
///     ENTITY_* values identify BillingInvoice, BillingPayment, and BillingRefund.
///
public class BillingAuditConstant
{
    public const string CREATE_INVOICE = "create_invoice";
    public const string UPDATE_DRAFT = "update_draft";
    public const string ISSUE_INVOICE = "issue_invoice";
    public const string CANCEL_INVOICE = "cancel_invoice";
    public const string ADD_LINE = "add_line";
    public const string UPDATE_LINE = "update_line";
    public const string DELETE_LINE = "delete_line";
    public const string ADD_DISCOUNT = "add_discount";
    public const string DELETE_DISCOUNT = "delete_discount";
    public const string UPDATE_BUYER = "update_buyer";
    public const string CREATE_PAYMENT = "create_payment";
    public const string CONFIRM_PAYMENT = "confirm_payment";
    public const string CANCEL_PAYMENT = "cancel_payment";
    public const string CREATE_REFUND = "create_refund";
    public const string WEBHOOK_PAYMENT = "webhook_payment";
    public const string ENTITY_INVOICE = "BillingInvoice";
    public const string ENTITY_PAYMENT = "BillingPayment";
    public const string ENTITY_REFUND = "BillingRefund";
}

///
/// Summary:
///     Defines the format of subscription invoice identifiers.
///
public class SubscriptionInvoiceConstant
{
    ///
    /// Summary:
    ///     Prefixes the compact GUID with SUB for subscription invoices.
    ///
    public const string CODE_FORMAT = "SUB{0:N}";
}
