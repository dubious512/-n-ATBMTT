namespace ATBMTT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string filePath = string.Empty;
        string salt = "fhdfjghdkjfhvudfhgdfhvkjdfhgjdfhgrugfsudhvjcbvjsdfhgsdjfghjsdf3476t346r7fuy73434737";
        string password;
        byte[] fileToEncrypt = null;
        byte[] encryptedFile = null;

        byte[] fileToDecrypt = null;
        byte[] decryptedFile = null;


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialog.FileName;
                textBox1.Text = filePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            password = string.Empty;
            fileToEncrypt = File.ReadAllBytes(filePath);
            password = textBox2.Text;
            encryptedFile = Crypto.getEncryptor(password, fileToEncrypt, salt);
            if (encryptedFile == null)
            {
                MessageBox.Show("Mật khẩu không hợp lệ cho thuật toán  này");
                return;
            }
            else
            {
                File.WriteAllBytes(filePath, encryptedFile);
                MessageBox.Show("Mã hóa thành công");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            password = string.Empty;
            fileToDecrypt = File.ReadAllBytes(filePath);
            password=textBox2.Text;
            decryptedFile = Crypto.getDecryptor(password, fileToDecrypt, salt);    
            if ( decryptedFile == null)
            {
                MessageBox.Show("Mật khẩu không hợp lệ cho thuật toán  này");
                return;
            }
            else
            {
                File.WriteAllBytes(filePath, decryptedFile);
                MessageBox.Show("Giải mã thành công");
            }

           

        }
    }
}