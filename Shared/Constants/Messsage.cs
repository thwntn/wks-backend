namespace Shared;

///
/// Summary:
///     Defines application error codes for consistent error handling and client-side processing.
///
public class ErrorCodeConstant
{
    ///
    /// Summary:
    ///     Error code for internal system failures.
    ///
    public const int PACKAGE_QUOTA_EXCEEDED = 1100;

    public const int SYSTEM_INTERNAL_ERROR = 2000;

    ///
    /// Summary:
    ///     Error code when confirmation code send limit is exceeded.
    ///
    public const int AUTH_CODE_LIMIT = 1000;

    ///
    /// Summary:
    ///     Error code when confirmation code cooldown period is active.
    ///
    public const int AUTH_CODE_COOLDOWN = 1001;

    ///
    /// Summary:
    ///     Error code for invalid authentication credentials.
    ///
    public const int AUTH_INVALID_IDENTIFIER = 1002;
}

///
/// Summary:
///     Contains user-facing message constants for API responses and system notifications.
///     Messages are used for error reporting, success confirmations, and status updates.
///
public class MessageConstant
{
    public const string ORDER_ISSUE_WAREHOUSE_AMBIGUOUS =
        "Không tìm thấy một kho xuất duy nhất cho cửa hàng online thuộc vùng của đơn hàng.";
    public const string ORDER_ISSUE_SKU_AMBIGUOUS =
        "Không xác định được một SKU kho duy nhất cho sản phẩm trong đơn hàng.";

    public const string PRINT_FORBIDDEN =
        "Bạn không có quyền truy cập hàng đợi in này";
    public const string PRINT_INVALID =
        "Nội dung hoặc cấu hình in không hợp lệ";
    public const string PRINT_NOT_FOUND = "Không tìm thấy tác vụ in";
    public const string PRINT_CONFLICT =
        "Tác vụ in đã được thay đổi hoặc lần nhận xử lý không còn hợp lệ";

    public const string GATEWAY_CONNECTOR_INVALID_CONFIGURATION =
        "Kết nối cổng thanh toán cần thông tin đơn vị kinh doanh, khóa bí mật, PURCHASE/VND và các URL nhận kết quả hợp lệ";
    public const string ORDER_WORKFLOW_STALE =
        "Trạng thái đơn hàng đã thay đổi. Vui lòng tải lại danh sách trước khi xử lý.";
    public const string SUBSCRIPTION_IDENTIFIER_INVALID =
        "Mã hồ sơ hoặc mã gói dịch vụ không hợp lệ";

    public const string SUBSCRIPTION_PACKAGE_ALREADY_ASSIGNED =
        "Hồ sơ này đã được cấp gói dịch vụ";

    public const string PACKAGE_PURCHASE_UNAVAILABLE =
        "Gói dịch vụ hiện không được mở bán";

    public const string PACKAGE_PRICE_INVALID = "Giá gói dịch vụ không hợp lệ";

    public const string PACKAGE_PRICE_CHANGED =
        "Giá gói dịch vụ đã thay đổi. Vui lòng tải lại trước khi mua";

    public const string WALLET_BALANCE_INSUFFICIENT = "Số dư ví không đủ";

    public const string WALLET_BALANCE_INVALID = "Số dư ví không hợp lệ";

    public const string WALLET_OPERATION_CONFLICT =
        "Dữ liệu ví đã thay đổi trong khi thực hiện thao tác. Vui lòng tải lại và thử lại.";

    public const string PACKAGE_QUOTA_EXCEEDED = "Đã vượt hạn mức gói dịch vụ";
    public const string INVALID_PROFILE_ID = "Mã hồ sơ không hợp lệ";
    public const string INVALID_QUOTA_DEFINITION =
        "Định nghĩa hạn mức không hợp lệ";
    public const string INVALID_QUOTA_AMOUNT = "Số lượng hạn mức không được âm";
    public const string INVALID_PACKAGE_QUOTA_LIMIT =
        "Giới hạn hạn mức của gói dịch vụ không được âm";
    public const string QUOTA_USAGE_OVERFLOW =
        "Mức sử dụng hạn mức vượt phạm vi số được hỗ trợ";
    public const string INVALID_REDIRECT_WEBHOOK_EVENT =
        "Sự kiện webhook chuyển hướng không hợp lệ";
    public const string QUOTA_NOT_FOUND = "Không tìm thấy hạn mức";
    public const string SUBSCRIPTION_NOT_FOUND =
        "Không tìm thấy đăng ký gói dịch vụ";

    public const string SYSTEM_INTERNAL_ERROR = "Lỗi nội bộ hệ thống";
    public const string NOT_FOUND_DOMAIN = "Không tìm thấy tên miền";
    public const string DOMAIN_TYPE_ALREADY_EXISTS = "Loại tên miền đã tồn tại";
    public const string DOMAIN_HOST_ALREADY_EXISTS =
        "Địa chỉ tên miền đã tồn tại";
    public const string NOT_FOUND_FILE = "Không tìm thấy tệp";
    public const string SYSTEM_SUBJECT_EMAIL_CONFIRM_CODE =
        "Xác minh tài khoản - đây là mã xác nhận của bạn";
    public const string NOT_FOUND_ID = "Không tìm thấy mã định danh";
    public const string NOT_FOUNT_ENVIRONMENT_FILE =
        "Không tìm thấy tệp cấu hình môi trường";
    public const string NOT_FOUND_ACCOUNT = "Không tìm thấy tài khoản";
    public const string NOT_FOUND_PROFILE = "Không tìm thấy hồ sơ";
    public const string NOT_FOUND_GATEWAY_CONNECTOR =
        "Không tìm thấy kết nối cổng thanh toán";
    public const string NOT_FOUND_GROUP = "Không tìm thấy nhóm";
    public const string TELEGRAM_SETUP_WEBHOOK_FAIL =
        "Thiết lập webhook Telegram thất bại";
    public const string NOT_FOUND_ROLE = "Không tìm thấy vai trò";
    public const string ALREADY_ASSIGN_ROLE = "Vai trò đã được gán";
    public const string ROLE_ALREADY_EXISTS = "Mã vai trò đã tồn tại";
    public const string NOT_ASSIGNED_ROLE = "Tài khoản chưa được gán vai trò";
    public const string NOT_FOUND_VERSION = "Không tìm thấy phiên bản";
    public const string VERSION_ALREADY_EXISTS = "Phiên bản đã tồn tại";
    public const string NOT_FOUND_REFRESH_TOKEN =
        "Không tìm thấy mã làm mới phiên đăng nhập";
    public const string JWT_CAN_NOT_READ_TOKEN = "Không thể đọc mã truy cập";
    public const string SESSION_IS_CLOSED_REFRESH_TOKEN =
        "Phiên đăng nhập đã kết thúc";
    public const string NOT_FOUND_ARTIFACT = "Không tìm thấy tài nguyên";
    public const string NOT_FOUND_OPTIONS = "Không tìm thấy tùy chọn";

    public const string NOT_FOUND_DATA_SET = "Không tìm thấy tập dữ liệu";
    public const string NOT_FOUND_EXPENSE_CATEGORY =
        "Không tìm thấy danh mục chi tiêu";
    public const string NOT_FOUND_CUSTOMER = "Không tìm thấy khách hàng";
    public const string NOT_FOUND_CUSTOMER_ADDRESS =
        "Không tìm thấy địa chỉ khách hàng";
    public const string NOT_FOUND_TAG = "Không tìm thấy nhãn";
    public const string TAG_ALREADY_EXISTS = "Nhãn đã tồn tại";
    public const string NOT_FOUND_EMAIL = "Không tìm thấy email";
    public const string NOT_FOUND_PAYMENT = "Không tìm thấy thanh toán";
    public const string NOT_FOUND_NOTE_PROCESS =
        "Không tìm thấy quy trình ghi chú";
    public const string NOT_FOUND_NOTE = "Không tìm thấy ghi chú";
    public const string PASSWORD_NOT_MATCH = "Mật khẩu xác nhận không khớp";
    public const string NOT_FOUND_DISCOUNT = "Không tìm thấy mã giảm giá";
    public const string LIMIT_DISCOUNT_VALUE =
        "Mã giảm giá đã hết lượt sử dụng";
    public const string DISCOUNT_EXPIRED = "Mã giảm giá đã hết hạn";
    public const string NOT_FOUND_KANBAN_CATEGORY =
        "Không tìm thấy danh mục bảng công việc";
    public const string NOT_FOUND_KANBAN = "Không tìm thấy bảng công việc";
    public const string NOT_FOUND_TOKEN = "Không tìm thấy mã truy cập";
    public const string INVALID_CUSTOM_TOKEN =
        "Đường dẫn tùy chỉnh chỉ được chứa chữ cái, chữ số, dấu gạch ngang và dấu gạch dưới";
    public const string CUSTOM_TOKEN_ALREADY_EXISTS =
        "Đường dẫn tùy chỉnh đã tồn tại";
    public const string REDIRECT_WEBHOOK_LIMIT_EXCEEDED =
        "Đã đạt số lượng webhook tối đa cho mỗi hồ sơ";
    public const string NOT_FOUND_DELIVERY =
        "Không tìm thấy bản ghi gửi webhook";
    public const string NOT_FOUND_PRODUCT = "Không tìm thấy sản phẩm";
    public const string NOT_FOUND_LISTING = "Không tìm thấy bài đăng sản phẩm";
    public const string LISTING_ALREADY_EXISTS = "Sản phẩm này đã có bài đăng";
    public const string PRODUCT_NAME_ALREADY_EXISTS = "Tên sản phẩm đã tồn tại";
    public const string PRODUCT_SLUG_ALREADY_EXISTS =
        "Đường dẫn sản phẩm đã tồn tại";
    public const string PRODUCT_SLUG_INVALID =
        "Đường dẫn sản phẩm không hợp lệ";
    public const string PRODUCT_SKU_MIN_LENGTH =
        "SKU sản phẩm phải có ít nhất 6 ký tự";
    public const string SKU_CODE_MIN_LENGTH = "Mã SKU phải có ít nhất 6 ký tự";
    public const string NOT_FOUND_RATE = "Không tìm thấy tỷ lệ";
    public const string NOT_FOUND_LINK = "Không tìm thấy liên kết";
    public const string LINK_EXPIRED = "Liên kết đã hết hạn";
    public const string REDIRECT_CAMPAIGN_NOT_FOUND =
        "Chuyển hướng của chiến dịch đã hết hạn";
    public const string NOT_FOUND_CAMPAIGN_REDIRECT =
        "Không tìm thấy chiến dịch chuyển hướng";
    public const string NOT_FOUND_TEMPLATE_REDIRECT =
        "Không tìm thấy mẫu chuyển hướng hoặc mẫu không thuộc hồ sơ của bạn";
    public const string REDIRECT_WEBHOOK_NOT_FOUND =
        "Không tìm thấy webhook chuyển hướng";
    public const string REDIRECT_TEMPLATE_MAX_ROWS_EXCEEDED =
        "Tệp vượt số dòng tối đa được phép để tạo liên kết";
    public const string NOT_FOUND_FEEDBACK = "Không tìm thấy phản hồi";
    public const string NOT_FOUND_VARIANT = "Không tìm thấy biến thể";
    public const string REQUIRED_PRICE_CHANGE_NOTE =
        "Vui lòng nhập ghi chú khi thay đổi giá";
    public const string REQUIRED_STOCK_CHANGE_NOTE =
        "Vui lòng nhập ghi chú khi thay đổi tồn kho";
    public const string NOT_FOUND_ODER = "Không tìm thấy đơn hàng";
    public const string NOT_FOUND_ODER_STATUS =
        "Không tìm thấy trạng thái đơn hàng";

    public const string NOT_FOUND_BRAND = "Không tìm thấy thương hiệu";
    public const string NOT_FOUND_UNIT = "Không tìm thấy đơn vị tính";
    public const string NOT_FOUND_WAREHOUSE = "Không tìm thấy kho";
    public const string NOT_FOUND_WAREHOUSE_LOCATION =
        "Không tìm thấy vị trí trong kho";
    public const string NOT_FOUND_SKU = "Không tìm thấy SKU";
    public const string NOT_FOUND_WAREHOUSE_SKU = "Không tìm thấy SKU";
    public const string NOT_FOUND_RECEIVING_LOCATION =
        "Không tìm thấy vị trí nhận hàng";
    public const string NOT_FOUND_GOODS_RECEIPT =
        "Không tìm thấy phiếu nhập hàng";
    public const string NOT_FOUND_GOODS_RECEIPT_LINE =
        "Không tìm thấy dòng hàng trong phiếu nhập";
    public const string NOT_FOUND_GOODS_ISSUE =
        "Không tìm thấy phiếu xuất hàng";
    public const string NOT_FOUND_GOODS_ISSUE_LINE =
        "Không tìm thấy dòng hàng trong phiếu xuất";
    public const string GOODS_RECEIPT_INVALID_STATUS =
        "Trạng thái phiếu nhập hàng không hợp lệ";
    public const string GOODS_RECEIPT_STOCK_INCREASE_NOTE =
        "Tăng tồn kho có thể bán từ phiếu nhập hàng";
    public const string GOODS_RECEIPT_LOCATION_CAPACITY_EXCEEDED =
        "Đã vượt sức chứa của vị trí lưu kho";
    public const string GOODS_RECEIPT_LOCATION_SKU_NOT_MIXABLE =
        "Vị trí lưu kho không cho phép chứa nhiều SKU khác nhau";
    public const string GOODS_ISSUE_INVALID_STATUS =
        "Trạng thái phiếu xuất hàng không hợp lệ";
    public const string INSUFFICIENT_INVENTORY = "Không đủ hàng tồn kho";
    public const string NOT_FOUND_WAREHOUSE_PRODUCT =
        "Không tìm thấy sản phẩm trong kho";
    public const string INVALID_INVENTORY_QUANTITY =
        "Số lượng tồn kho phải lớn hơn 0";
    public const string REQUIRED_INVENTORY_SOURCE_LOCATION =
        "Vui lòng chọn vị trí nguồn";
    public const string REQUIRED_INVENTORY_DESTINATION_LOCATION =
        "Vui lòng chọn vị trí đích";
    public const string NOT_FOUND_INTERNAL_TRANSFER =
        "Không tìm thấy phiếu chuyển vị trí nội bộ";
    public const string NOT_FOUND_WAREHOUSE_TRANSFER =
        "Không tìm thấy phiếu chuyển kho";
    public const string NOT_FOUND_STOCK_COUNT = "Không tìm thấy phiếu kiểm kê";
    public const string NOT_FOUND_INVENTORY_ADJUSTMENT =
        "Không tìm thấy phiếu điều chỉnh tồn kho";
    public const string INVALID_LOGISTICS_DOCUMENT_STATUS =
        "Trạng thái phiếu không cho phép thực hiện thao tác này";
    public const string INVALID_LOGISTICS_DOCUMENT_LINES =
        "Phiếu phải có ít nhất một dòng hàng";
    public const string INVALID_INVENTORY_ADJUSTMENT_REASON =
        "Lý do điều chỉnh tồn kho không hợp lệ";
    public const string LOGISTICS_WAREHOUSE_MUST_DIFFER =
        "Kho nguồn và kho đích phải khác nhau";
    public const string LOGISTICS_LOCATION_MUST_DIFFER =
        "Vị trí nguồn và vị trí đích phải khác nhau";
    public const string NOT_FOUND_MEMBERSHIP =
        "Không tìm thấy thông tin thành viên";
    public const string NOT_FOUND_CONSENT =
        "Không tìm thấy nội dung chấp thuận";
    public const string NOT_FOUND_CUSTOMER_CONSENT =
        "Không tìm thấy chấp thuận của khách hàng";
    public const string CUSTOMER_CONSENT_ALREADY_EXISTS =
        "Chấp thuận của khách hàng đã tồn tại";

    public const string NOT_FOUND_EXPENSE = "Không tìm thấy danh mục chi tiêu";
    public const string NOT_FOUND_INVOICE = "Không tìm thấy hóa đơn";

    public const string NOT_FOUNT_FILTER = "Không tìm thấy bộ lọc";
    public const string NOT_FOUND_CONDITION = "Không tìm thấy điều kiện";
    public const string FILTER_ROOT_GROUP_EXISTS =
        "Nhóm gốc của bộ lọc đã tồn tại";
    public const string FILTER_CONDITION_MAX_DEPTH =
        "Điều kiện lọc không được lồng quá 3 cấp";
    public const string CONDITION_HAS_CHILDREN =
        "Không thể xóa điều kiện đang có điều kiện con";

    public const string NOT_FOUND_OPEN_INTEGRATE_SESSION =
        "Không tìm thấy phiên tích hợp đang mở";
    public const string NOT_FOUND_INVOICE_STATUS =
        "Không tìm thấy trạng thái hóa đơn";
    public const string NOT_FOUND_CASH = "Không tìm thấy tiền mặt";

    public const string NOT_FOUND_CONTENT = "Không tìm thấy nội dung";
    public const string CONTENT_IS_PINNED = "Nội dung đã được ghim";
    public const string CONTENT_IS_NOT_PINNED = "Nội dung chưa được ghim";
    public const string CONTENT_ZNS_GET_TEMPLATE_FAILED =
        "Không thể lấy mẫu ZNS";

    public const string BROADCAST_IS_RUNNING =
        "Không thể kích hoạt khi chiến dịch gửi tin đang chạy";
    public const string NOT_FOUND_BROADCAST =
        "Không tìm thấy chiến dịch gửi tin";
    public const string BROADCAST_IS_PINNED = "Chiến dịch gửi tin đã được ghim";
    public const string BROADCAST_IS_NOT_PINNED =
        "Chiến dịch gửi tin chưa được ghim";

    public const string NOT_FOUND_AUTOMATION =
        "Không tìm thấy quy trình tự động";
    public const string NOT_FOUND_NODE = "Không tìm thấy nút";
    public const string NOT_FOUND_SETUP_AUTOMATION =
        "Không tìm thấy cấu hình quy trình tự động";
    public const string AUTOMATION_NOT_EDITABLE =
        "Chỉ có thể chỉnh sửa quy trình tự động ở trạng thái nháp hoặc đã dừng";
    public const string NOT_FOUND_OPERATOR = "Không tìm thấy toán tử";

    public const string NOT_FOUND_SEND_KEY = "Không tìm thấy khóa gửi";
    public const string BROADCAST_MAPPING_FIELD_FAILED =
        "Chưa hoàn tất cấu hình ánh xạ trường dữ liệu";

    public const string NOT_FOUND_DATA = "Không tìm thấy dữ liệu";
    public const string DATA_IS_NOT_PINNED = "Dữ liệu chưa được ghim";
    public const string DATA_IS_PINNED = "Dữ liệu đã được ghim";
    public const string NOT_FOUND_FILTER = "Không tìm thấy bộ lọc";
    public const string NOT_FOUND_DEFAULT_FILTER =
        "Không tìm thấy bộ lọc mặc định";

    public const string CANNOT_REMOVE_FILTER =
        "Không thể xóa bộ lọc mặc định hoặc đang được sử dụng";

    public const string NOT_FOUND_FEATURE = "Không tìm thấy tính năng";

    public const string IMPORT_EXCEL_SUCCESS =
        "Nhập tệp vào hệ thống thành công";
    public const string IMPORT_CONCAT_SCHEMA_NOT_MATCH =
        "Cấu trúc dữ liệu ghép nối không khớp";
    public const string DATA_EXCEL_WRONG_FORMAT =
        "Định dạng hoặc cấu trúc tệp không được hệ thống hỗ trợ. Vui lòng kiểm tra lại tệp";

    public const string NOT_FOUND_CART = "Không tìm thấy giỏ hàng";
    public const string NOT_FOUND_CART_OR_EMPTY =
        "Không tìm thấy giỏ hàng hoặc giỏ hàng đang trống";
    public const string NOT_FOUND_CART_PRODUCT =
        "Không tìm thấy sản phẩm trong giỏ hàng";

    public const string NOT_FOUND_CATEGORY = "Không tìm thấy danh mục";

    public const string NOT_FOUND_CREDENTIAL =
        "Không tìm thấy thông tin xác thực";

    public const string NOT_FOUND_TELEGRAM_CREDENTIAL =
        "Không tìm thấy thông tin xác thực Telegram";

    public const string CANT_GENERATE = "Không thể tạo dữ liệu từ văn bản";
    public const string CANT_GENERATE_TRANSACTION =
        "Không thể tạo dữ liệu từ văn bản";

    public const string NOT_FOUND_ICON = "Không tìm thấy biểu tượng";
    public const string NOT_FOUND_SIGNAGE_MEDIA =
        "Không tìm thấy nội dung truyền thông màn hình";
    public const string NOT_FOUND_SIGNAGE_PLAYLIST =
        "Không tìm thấy danh sách phát màn hình";
    public const string NOT_FOUND_SIGNAGE_PLAYLIST_ASSET =
        "Không tìm thấy tài nguyên trong danh sách phát màn hình";
    public const string NOT_FOUND_SIGNAGE_SCREEN =
        "Không tìm thấy màn hình trình chiếu";
    public const string NOT_FOUND_BANNER = "Không tìm thấy biểu ngữ";
    public const string NOT_ATTACH = "Không tìm thấy tệp đính kèm";
    public const string NOT_FOUNT_FUNDS = "Không tìm thấy quỹ";
    public const string NOT_COMMENT = "Không tìm thấy bình luận";
    public const string NOT_FOUND_STORAGE = "Không tìm thấy kho lưu trữ";
    public const string NOT_FOUND_CONFIG_ZALO =
        "Không tìm thấy cấu hình xác thực Zalo";
    public const string NOT_FOUND_CONFIG_EMAIL =
        "Không tìm thấy cấu hình xác thực email";
    public const string NOT_FOUND_CONFIG_WEB_PUSH =
        "Không tìm thấy cấu hình thông báo đẩy trên web";
    public const string NOT_FOUND_PUSH_SUBSCRIPTION =
        "Không tìm thấy đăng ký nhận thông báo đẩy";
    public const string INVALID_DEFAULT_PERMISSION =
        "DEFAULT_PERMISSION phải chứa các mã quyền hợp lệ và không thuộc quyền quản trị";

    public const string NOT_FOUND_SETTING = "Không tìm thấy thiết lập";
    public const string SETTING_NAME_ALREADY_EXISTS =
        "Tên thiết lập đã tồn tại";
    public const string INVALID_DEFAULT_PACKAGE =
        "Phải có đúng một gói dịch vụ mặc định đang hoạt động";
    public const string NOT_FOUND_PACKAGE = "Không tìm thấy gói dịch vụ";
    public const string PACKAGE_NAME_ALREADY_EXISTS =
        "Tên gói dịch vụ đã tồn tại";
    public const string NOT_FOUND_PACKAGE_QUOTA =
        "Không tìm thấy hạn mức gói dịch vụ";
    public const string NOT_FOUND_PLAN = "Không tìm thấy kế hoạch";
    public const string NOT_FOUND_SALE_PROGRAM =
        "Không tìm thấy chương trình bán hàng";
    public const string NOT_FOUND_TASK_DETAIL =
        "Không tìm thấy chi tiết công việc";
    public const string NOT_FOUND_PROMOTIONAL =
        "Không tìm thấy chương trình khuyến mãi";
    public const string NOT_FOUND_NOTIFICATION = "Không tìm thấy thông báo";
    public const string NOT_FOUND_TELEGRAM_SETUP =
        "Không tìm thấy cấu hình Telegram";
    public const string WEBHOOK_RECEIVED_SUCCESS = "Đã nhận webhook thành công";
    public const string MESSAGE_RECEIVED_TRANSACTION_SUCCESS =
        "Thông báo: Biến động số dư tài khoản ngân hàng - Mã hoá đơn __INVOICE_CODE__ - Số tiền <u>__AMOUNT__</u>";
    public const string NOT_FOUND_EXPENSE_TRANSACTION =
        "Không tìm thấy khoản chi";
    public const string NOT_ACCEPT_ROLE =
        "Bạn không có quyền thực hiện thao tác này";
    public const string NOT_FOUND_ACTION_TYPE = "Không tìm thấy loại thao tác";
    public const string EMAIL_EXIST = "Email đã tồn tại";
    public const string CREDENTIAL_EXIST = "Thông tin xác thực đã tồn tại";
    public const string PHONE_EXIST = "Số điện thoại đã tồn tại";
    public const string GOOGLE_CANNOT_INSERT_DATABASE =
        "Không thể lưu thông tin Google vào cơ sở dữ liệu";
    public const string GOOGLE_NOT_FOUND_ACCESS_TOKEN =
        "Không tìm thấy mã truy cập";
    public const string GOOGLE_NOT_FOUND_PROFILE = "Không tìm thấy hồ sơ";
    public const string ZALO_NOT_FOUND_ACCESS_TOKEN =
        "Không tìm thấy mã truy cập Zalo";
    public const string ZALO_NOT_FOUND_PROFILE = "Không tìm thấy hồ sơ Zalo";
    public const string REQUEST_ACCEPTED =
        "Yêu cầu đã được chấp nhận, không thể chấp nhận lại";
    public const string REQUEST_SUCCESS = "Yêu cầu thành công";
    public const string FULL_SPACE = "Không còn dung lượng trống";
    public const string CONFIRM_CODE_NOT_SUCCESS = "Mã xác nhận không hợp lệ";
    public const string CONFIRM_CODE_SUCCESS = "Xác nhận thành công";
    public const string CANNOT_REMOVE_DISCOUNT =
        "Không thể xóa mã giảm giá đã được sử dụng trong hóa đơn";
    public const string CANNOT_ACTION = "Không thể thực hiện thao tác này";
    public const string TRANSACTION_SUCCESS = "Giao dịch thành công";
    public const string TRANSACTION_DUPLICATE_REQUEST =
        "Mã giao dịch đã tồn tại";

    public const string EXPENSE_IS_PINNED = "Khoản chi đã được ghim";
    public const string EXPENSE_IS_NOT_PINNED = "Khoản chi chưa được ghim";

    public const string CAMPAIGN_TEMPLATE_NOT_FOUND =
        "Không tìm thấy mẫu chiến dịch hoặc mẫu không thuộc hồ sơ của bạn";

    public const string NOT_FOUND_IMPORT = "Không tìm thấy bản ghi";
    public const string DATA_TYPE_NOT_SUPPORTED =
        "Thao tác này chỉ hỗ trợ dữ liệu nhập từ Excel hoặc dữ liệu khởi tạo";
    public const string INITIAL_COLUMNS_REQUIRED =
        "Vui lòng khai báo các cột cho dữ liệu khởi tạo";
    public const string WEBHOOK_URL_NOT_ALLOWED =
        "URL webhook không được phép sử dụng. Địa chỉ nội bộ bị chặn";
    public const string CAMPAIGN_END_DATE_INVALID =
        "Ngày kết thúc phải sau ngày bắt đầu";

    ///
    /// Summary:
    ///     Business POS store, merchant, currency and region management.
    ///
    public const string POS_REGION_HAS_STORES =
        "Không thể xóa vùng đang có cửa hàng. Hãy chuyển tất cả cửa hàng sang vùng khác trước.";
    public const string NOT_FOUND_POS_REGION = "Không tìm thấy vùng.";
    public const string INVALID_POS_REGION = "Thông tin vùng không hợp lệ.";
    public const string POS_REGION_HAS_ORDERS =
        "Không thể xóa vùng đã được sử dụng trong đơn hàng.";
    public const string INVALID_CHECKOUT_REGION =
        "Vùng hoặc thông tin cửa hàng không hợp lệ. Vui lòng chọn lại vùng trước khi thanh toán.";
    public const string POS_REGION_HAS_ONLINE_STORE =
        "Vùng đã có cửa hàng online. Mỗi vùng chỉ được có một cửa hàng online.";
    public const string NOT_FOUND_POS_MERCHANT =
        "Không tìm thấy đơn vị kinh doanh POS";
    public const string POS_MERCHANT_ALREADY_EXISTS =
        "Hồ sơ này đã có đơn vị kinh doanh POS";
    public const string NOT_FOUND_POS_STORE = "Không tìm thấy cửa hàng POS";
    public const string NOT_FOUND_POS_CURRENCY =
        "Không tìm thấy đơn vị tiền tệ POS";

    public const string NOT_FOUND_BILLING_INVOICE =
        "Không tìm thấy hóa đơn thanh toán";
    public const string NOT_FOUND_BILLING_LINE =
        "Không tìm thấy dòng hàng trong hóa đơn";
    public const string NOT_FOUND_BILLING_PAYMENT =
        "Không tìm thấy khoản thanh toán hóa đơn";
    public const string NOT_FOUND_BILLING_TAX_RATE =
        "Không tìm thấy thuế suất hóa đơn";
    public const string NOT_FOUND_BILLING_SEQUENCE =
        "Không tìm thấy dãy số hóa đơn";
    public const string NOT_FOUND_BILLING_DISCOUNT =
        "Không tìm thấy giảm giá hóa đơn";
    public const string NOT_FOUND_ORDER = "Không tìm thấy đơn hàng";
    public const string INVALID_BILLING_INVOICE_STATUS =
        "Trạng thái hóa đơn không cho phép thực hiện thao tác này";
    public const string BILLING_INVOICE_ALREADY_ISSUED =
        "Hóa đơn đã được phát hành";
    public const string BILLING_INVOICE_EMPTY =
        "Hóa đơn phải có ít nhất một dòng hàng";
    public const string INVALID_BILLING_PAYMENT_AMOUNT =
        "Số tiền thanh toán phải lớn hơn 0";
    public const string PAYMENT_EXCEEDS_BALANCE =
        "Số tiền thanh toán vượt số dư còn phải trả của hóa đơn";
    public const string NOT_FOUND_BILLING_BANK_ACCOUNT =
        "Không tìm thấy tài khoản ngân hàng thanh toán";
    public const string REQUIRED_BILLING_BANK_ACCOUNT =
        "Thanh toán chuyển khoản yêu cầu tài khoản ngân hàng của đơn vị kinh doanh";
    public const string BILLING_BANK_ACCOUNT_INACTIVE =
        "Tài khoản ngân hàng thanh toán không hoạt động";
    public const string BILLING_BANK_ACCOUNT_STORE_MISMATCH =
        "Tài khoản ngân hàng thanh toán không thuộc cửa hàng của hóa đơn";
    public const string NOT_FOUND_BILLING_REFUND =
        "Không tìm thấy khoản hoàn tiền hóa đơn";
    public const string INVALID_BILLING_REFUND_AMOUNT =
        "Số tiền hoàn phải lớn hơn 0";
    public const string REFUND_EXCEEDS_PAID =
        "Số tiền hoàn vượt số tiền đã thanh toán của hóa đơn";
    public const string BILLING_INVOICE_HAS_PAYMENT =
        "Hóa đơn đã có thanh toán phải được hoàn tiền trước khi hủy";
    public const string BILLING_REFUND_NOT_PENDING =
        "Trạng thái hoàn tiền không cho phép thực hiện thao tác này";
    public const string NOT_FOUND_BILLING_WEBHOOK_PAYMENT =
        "Không tìm thấy khoản thanh toán hóa đơn tương ứng với nội dung webhook";
    public const string ADMIN_PASS_INVALID = "Mật khẩu quản trị không hợp lệ";
}
