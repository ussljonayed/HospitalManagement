namespace HospitalManagement.Models.ViewModels
{
    public class Pagination
    {
        //input properties
        public int TotalRecords { set; get; }
        public int PageSize { set; get; }
        public int CurrentPage { set; get; }

        //calculative properties
        public int FastPage { set; get; } = 1;
        public int LastPage { set; get; }
        public int PreviousPage { set; get; }
        public int NextPage { set; get; }
        public int TotalPages { set; get; }
        public int Skip { set; get; }


        public Pagination(int TotalRecordes, int CurrentPage = 1, int PageSize = 2)
        {
            this.CurrentPage = CurrentPage;
            this.TotalRecords = TotalRecordes;
            this.PageSize = PageSize;

            //Calculated Properties
            this.TotalPages = (int)Math.Ceiling(this.TotalRecords / (double)this.PageSize);
            this.Skip = (CurrentPage - 1) * PageSize;
            this.NextPage = (CurrentPage < TotalPages) ? CurrentPage + 1 : TotalPages;
            this.PreviousPage = (CurrentPage > 1) ? CurrentPage - 1 : 1;
            this.LastPage = TotalPages;
        }
    }
}
