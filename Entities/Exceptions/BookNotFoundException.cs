namespace Entities.Exceptions
{
    // sealed ile zırhlanır kalıtıma kapatıldı
   public sealed class BookNotFoundException : NotFoundeException
   {
      public BookNotFoundException(int id) : base($"The book with id:{id} could not found.")
      {

      }
    }
    
}
