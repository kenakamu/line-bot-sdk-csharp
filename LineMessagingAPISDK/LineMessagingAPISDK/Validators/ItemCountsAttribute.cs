using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineMessagingAPISDK.Validators
{
    public class ItemCountsAttribute : ValidationAttribute
    {
        private int maxCount;
        public ItemCountsAttribute(int maxCount)
        {
            this.maxCount = maxCount;
        }

        public override bool Equals(object obj)
        {
            var list = obj as IList;
            if (list != null)
            {
                return list.Count <= maxCount;
            }
            return true;
        }
    }
}
