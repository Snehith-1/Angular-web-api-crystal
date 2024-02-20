using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ems.hrm.Models
{
 
    public class MdlHRDocument:result
    {
        
        public List<hrdocument_list> hrdocument_list { get; set; }
    }
    public class hrdocument_list
    {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string esigndoc_expiryflag { get; set; }
        public string api_code { get; set; }
    }
    public class hrdocument : result
    {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class SysHRDocumentInactiveHistory : result
    {
        public List<hrdocumentinactivehistory_list> hrdocumentinactivehistory_list { get; set; }
    }
    public class hrdocumentinactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    //E Signing - Document Details
    public class MdlFileDetailsEsign : result
    {
        public string hrdoc_id { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string migration_flag { get; set; }
    }

    //E Signing - Uploading Document to Digio Request
    public class MdlUploadDocumenttoDigioRequest
    {
        public UploadDocumenttoDigioRequestsigners[] signers { get; set; }

        public string expire_in_days { get; set; }
        public string comment { get; set; }
        public string display_on_page { get; set; }
        public string send_sign_link { get; set; }
        public string notify_signers { get; set; }
    }

    public class UploadDocumenttoDigioRequestsigners
    {
        public string identifier { get; set; }
        public string name { get; set; }
        public string reason { get; set; }
    }

    //E Signing - Uploading Document to Digio Response
    public class MdlUploadDocumenttoDigioResponse
    {

        public string id { get; set; }
        public string is_agreement { get; set; }
        public string agreement_type { get; set; }
        public string agreement_status { get; set; }
        public string file_name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string self_signed { get; set; }
        public string self_sign_type { get; set; }
        public string no_of_pages { get; set; }

        public UploadDocumenttoDigioResponsesigning_parties[] signing_parties { get; set; }

        public UploadDocumenttoDigioResponsesign_request_details sign_request_details { get; set; }

        public string channel { get; set; }

        public UploadDocumenttoDigioResponsesign_other_doc_details sign_other_doc_details { get; set; }

        public UploadDocumenttoDigioResponsesign_attached_estamp_details sign_attached_estamp_details { get; set; }

    }

    public class UploadDocumenttoDigioResponsesigning_parties
    {
        public string name { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string signature_type { get; set; }
        public string identifier { get; set; }
        public string reason { get; set; }
        public string expire_on { get; set; }
    }

    public class UploadDocumenttoDigioResponsesign_request_details
    {
        public string name { get; set; }
        public string requested_on { get; set; }
        public string expire_on { get; set; }
        public string identifier { get; set; }
        public string requester_type { get; set; }
    }

    public class UploadDocumenttoDigioResponsesign_other_doc_details
    {

    }

    public class UploadDocumenttoDigioResponsesign_attached_estamp_details
    {

    }

    public class HrDocumentImportdtl
    {
        public string employee_code { get; set; }
        public string document_id { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
    }

    public class MdlExcelSheetInfo : result
    {
        public List<MdlExcelSheetInfo_list> MdlExcelSheetInfo_list { get; set; }
    }
    public class MdlExcelSheetInfo_list
    {
        public string sheetName { get; set; }
        public string endRange { get; set; }
        public int rowCount { get; set; }
        public int columnCount { get; set; }
    }
    public class Mdlemployee_list
    {
        public string employee_gid { get; set; }
        public string user_code { get; set; }
    }
}

    
