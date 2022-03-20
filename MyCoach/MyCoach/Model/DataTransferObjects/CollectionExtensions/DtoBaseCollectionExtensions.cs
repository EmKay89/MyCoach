using System.Collections.Generic;

namespace MyCoach.Model.DataTransferObjects.CollectionExtensions
{
    public static class DtoBaseCollectionExtensions
    {
        public static void ResetSubscriptions(this IEnumerable<DtoBase> dtos)
        {
            foreach (var dto in dtos)
            {
                dto.ResetSubscriptions();
            }
        }
    }
}
