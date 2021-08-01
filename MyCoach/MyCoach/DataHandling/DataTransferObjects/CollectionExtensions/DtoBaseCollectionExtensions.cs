﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects.CollectionExtensions
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

        public static void Foreach<T>(this IEnumerable<T> dtos, Action<T> action)
        {
            foreach (var dto in dtos)
            {
                action(dto);
            }
        }
    }
}