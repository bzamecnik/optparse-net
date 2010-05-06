using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public abstract class StorageAction<ValueType> : ParametrizedAction<ValueType>
    {
        #region Construction

        protected StorageAction(OptionValue<ValueType> destination)
        {
            Destination = destination;
        }

        #endregion

        #region Public properties

        public OptionValue<ValueType> Destination { get; protected set; }

        #endregion
    }
}
