

namespace Entities.Exceptions
{
    //abstract ile newleme yapılmazz
    public abstract class NotFoundeException : Exception
    {
        protected NotFoundeException(string message) :base(message)
        {
            
        }
    }
}
