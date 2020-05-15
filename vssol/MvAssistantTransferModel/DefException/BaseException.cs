using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistantTransferModel.DefException
{
   public  abstract class BaseException:Exception
    {
        public BaseException()
        {

        }
        public BaseException(string message):base(message)
        {

        }
    }
}
