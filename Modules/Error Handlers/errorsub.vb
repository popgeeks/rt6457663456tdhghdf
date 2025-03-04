Imports MySql.Data.MySqlClient

Module errorhandler
    Public Sub errorsub(ex As Exception, procedure As String)
        Dim oMySqlHelper = New MySQLFactory.MySQLDBHelper()
        Dim oDataSet = New DataSet()

        Dim arParms() As MySqlParameter = New MySqlParameter(3) {}
        arParms(0) = New MySqlParameter("?iErrorCode", 999)
        arParms(1) = New MySqlParameter("?sErrorMessage", ex.Message)
        arParms(2) = New MySqlParameter("?sErrorStackTrace", ex.StackTrace)
        arParms(3) = New MySqlParameter("?sErrorPage", "TTE Server")

        oMySqlHelper.FillDataSet(PortalConnectionString, CommandType.StoredProcedure, "spi_Error", oDataSet, New String() {"Results"}, arParms)
        
        frmserver.TextBox1.Text += String.Format("{0} >> {1}", procedure, ex.StackTrace) + vbCrLf
    End Sub

    Public Sub errorsub(ex As String, procedure As String)
        Dim oMySqlHelper = New MySQLFactory.MySQLDBHelper()
        Dim oDataSet = New DataSet()

        Dim arParms() As MySqlParameter = New MySqlParameter(3) {}
        arParms(0) = New MySqlParameter("?iErrorCode", 999)
        arParms(1) = New MySqlParameter("?sErrorMessage", ex)
        arParms(2) = New MySqlParameter("?sErrorStackTrace", procedure)
        arParms(3) = New MySqlParameter("?sErrorPage", "TTE Server")

        oMySqlHelper.FillDataSet(PortalConnectionString, CommandType.StoredProcedure, "spi_Error", oDataSet, New String() {"Results"}, arParms)

        frmserver.TextBox1.Text += String.Format("{0} >> {1}", procedure, ex) + vbCrLf
    End Sub
End Module