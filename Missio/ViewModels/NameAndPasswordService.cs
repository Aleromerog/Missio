using Domain;

namespace ViewModels
{
    public class NameAndPasswordService : INameAndPasswordService
    {
        /// <inheritdoc />
        public NameAndPassword NameAndPassword { get; set; }
    }
}