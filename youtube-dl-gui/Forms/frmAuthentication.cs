﻿namespace youtube_dl_gui;
using System.Runtime.InteropServices;
using System.Windows.Forms;
public partial class frmAuthentication : Form {

    /// <summary>
    /// Gets or sets the authentication data for the instance.
    /// </summary>
    public AuthenticationDetails Authentication { get => AuthenticationData; set => AuthenticationData = value; }
    private AuthenticationDetails AuthenticationData;

    private bool ChangingText = false;

    //public string Username;
    //public string Password;
    //public string TwoFactor;
    //public string VideoPassword;
    //public bool Netrc;

    public frmAuthentication() {
        InitializeComponent();
        LoadLanguage();
        CalculatePositions();
    }
    public frmAuthentication(AuthenticationDetails Details) : this() => AuthenticationData = Details;

    private void LoadLanguage() {
        this.Text = Language.frmAuthentication;
        lbAuthNotice.Text = Language.lbAuthNotice;
        lbAuthUsername.Text = Language.lbAuthUsername;
        lbAuthPassword.Text = Language.lbAuthPassword;
        lbAuth2Factor.Text = Language.lbAuth2Factor;
        lbAuthVideoPassword.Text = Language.lbAuthVideoPassword;
        chkAuthUseNetrc.Text = Language.chkAuthUseNetrc;
        lbAuthCookiesFromFile.Text = Language.lbAuthCookiesFromFile;
        lbAuthCookiesFromBrowser.Text = Language.lbAuthCookiesFromBrowser;
        lbAuthNoSave.Text = Language.lbAuthNoSave;
        btnAuthBeginDownload.Text = Language.btnAuthBeginDownload;
        btnAuthGenericCancel.Text = Language.GenericCancel;
    }
    private void CalculatePositions() {
        chkAuthUseNetrc.Location = new(
            //(this.Size.Width - chkAuthUseNetrc.Size.Width) / 2,
            (this.ClientSize.Width - chkAuthUseNetrc.Size.Width) / 2,
            chkAuthUseNetrc.Location.Y
        );
    }

    private void chkPasswordVisible_CheckedChanged(object sender, EventArgs e) {
        txtPassword.PasswordChar = chkPasswordVisible.Checked ? '\0' : '●';
    }
    private void chkVideoPassVisible_CheckedChanged(object sender, EventArgs e) {
        txtVideoPassword.PasswordChar = chkVideoPassVisible.Checked ? '\0' : '●';
    }
    private void btnAuthBeginDownload_Click(object sender, EventArgs e) {
        AuthenticationData = new();

        if (!txtUsername.Text.IsNullEmptyWhitespace()) {
            AuthenticationData.Username = txtUsername.Text;
            txtUsername.Text = string.Empty;
        }
        if (!txtPassword.Text.IsNullEmptyWhitespace()) {
            AuthenticationData.Password = new();

            for (int i = 0; i < txtPassword.Text.Length; i++)
                AuthenticationData.Password.AppendChar(txtPassword.Text[i]);

            AuthenticationData.Password.MakeReadOnly();

            txtPassword.Text = string.Empty;
        }
        if (!txt2Factor.Text.IsNullEmptyWhitespace()) {
            AuthenticationData.TwoFactor = txt2Factor.Text;
            txt2Factor.Text = string.Empty;
        }
        if (!txtVideoPassword.Text.IsNullEmptyWhitespace()) {
            AuthenticationData.MediaPassword = new();

            for (int i = 0; i < txtVideoPassword.Text.Length; i++)
                AuthenticationData.MediaPassword.AppendChar(txtVideoPassword.Text[i]);

            AuthenticationData.MediaPassword.MakeReadOnly();

            txtVideoPassword.Text = string.Empty;
        }
        if (!txtCookiesFile.Text.IsNullEmptyWhitespace() && System.IO.File.Exists(txtCookiesFile.Text)) {
            AuthenticationData.CookiesFile = txtCookiesFile.Text;
            txtCookiesFile.Text = string.Empty;
        }
        if (!txtCookiesFromBrowser.Text.IsNullEmptyWhitespace()) {
            AuthenticationData.CookiesFromBrowser = txtCookiesFromBrowser.Text;
            txtCookiesFromBrowser.Text = string.Empty;
        }
        AuthenticationData.NetRC = chkAuthUseNetrc.Checked;

        this.DialogResult = DialogResult.OK;
    }
    private void btnAuthGenericCancel_Click(object sender, EventArgs e) {
        txtUsername.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txt2Factor.Text = string.Empty;
        txtVideoPassword.Text = string.Empty;
        chkAuthUseNetrc.Checked = false;
        this.DialogResult = DialogResult.Cancel;
    }

    private void llCookiesFromBrowserHint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {

    }
}