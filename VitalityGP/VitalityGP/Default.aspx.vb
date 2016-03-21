Imports DevExpress.Xpo

Public Class _Default1
    Inherits System.Web.UI.Page

    Private Sub _Default1_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Dim session As Session = XpoHelper.GetNewSession()
        'XpoDataSource2.Session = session


        Dim session As Session = XpoHelper2.GetNewSession(XpoHelper2.Database.XpoWebTest)
        XpoDataSource2.Session = session



    End Sub

   

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim u As MembershipUser
        u = Membership.GetUser(User.Identity.Name)

        'get the GPID from CRm from the profile object
        Dim intPersonID = Context.Profile.GetPropertyValue("PersonID")

        Try
            'Equivalent of an Insert Statement in SQL
            Using uow = New UnitOfWork()

                Dim save As New vitop(uow)
                save.Title = cboTitle.SelectedItem.Value
                save.Firstname = txtFirstName.Text
                save.Lastname = txtLastname.Text
                save.DOB = dtDOB.Text
                save.Gender = cboGender.SelectedItem.Value
                save.Address1 = txtAddress1.Text
                save.Address2 = txtAddress2.Text
                save.Address3 = txtAddress3.Text
                save.Postcode = txtPostcode.Text
                save.Telephone = txtTelephone.Text
                save.LeaveMessage = chkLeaveMessage.Checked.ToString
                save.ConsentThirdParty = chkThirdParty.Checked.ToString
                save.PolicyNumber = txtPolicyNumber.Text
                'save.AuthNumber = txtAuthnumber.Text
                save.PresentingCondition = txtPresentingCondition.Text
                save.Symptoms = txtSymptoms.Text

                If spinExcess.Text = "" Then
                    save.Excess = 0
                Else
                    save.Excess = spinExcess.Text
                End If
                If spinOutPatientLimit.Text = "" Then
                    save.OutpatientLimit = 0
                Else
                    save.OutpatientLimit = spinOutPatientLimit.Text
                End If

                save.GPSelect = cboGPSelect.SelectedItem.Value
                save.AltContact = txtAltContact.Text

                If cboGPSelect.SelectedItem.Value = "No" Then
                    ' we only want to pass a Hospital value if GPSelect is No
                    save.HospitalList = cboHospitalList.SelectedItem.Value
                Else
                    save.HospitalList = DBNull.Value.ToString
                End If


                save.Callback = dtCallBack.Value
                save.SelfPaying = chkSelfPay.Checked.ToString

                'check for optional(null) callback time
                'If Not dtCallback.Text = "" Then
                '    Dim dateold = DateTime.Parse(dtCallback.DateTime)
                ' Dim dtNew = New Date(Now.Year, Now.Month, Now.Day, dateold.Hour, dateold.Minute, dateold.Millisecond)
                ' save.Callback = dtNew
                '   End If
                save.userID = u.ProviderUserKey()
                save.GPID = intPersonID 'gpid
                save.Save()

                '.Save() will persistent your changes to the object but uow.CommitChanges() will save those changes back to the database
                uow.CommitChanges()
            End Using


        Catch ex As Exception
            MsgBox(ex.Message)
            '  errorMessageLabel.Visible = True
        End Try
    End Sub

    Private Sub _Default1_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

    End Sub

    Private Sub dtCallBack_Init(sender As Object, e As EventArgs) Handles dtCallBack.Init
        dtCallBack.Date = Now.AddHours(2)
        dtCallBack.MinDate = Now.AddHours(2)
    End Sub

  
End Class