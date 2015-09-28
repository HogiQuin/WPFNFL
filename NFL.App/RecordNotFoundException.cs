using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class RecordNotFoundException : Exception
{
    #region attributes

    private string _errorMessage;

    #endregion

    #region properties

    public string ErrorMessage
    {
        get { return _errorMessage; }
    }

    #endregion

    #region constructor

    public RecordNotFoundException()
    {
        _errorMessage = "Record not found";
    }

    #endregion

}
