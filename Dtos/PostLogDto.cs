namespace Unify.UNIFY.Dtos
{
    public class PostLogDto
    {
         public string Id{get;set;}
        public string PostId{get;set;}
        public string MemberName{get;set;}
        public string PostUrl{get;set;}
    }

     public class CreatePostLogRequestModel
    {
       public string PostUrl{get;set;} 
       public string PostId{get;set;}
    }
}