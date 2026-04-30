using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyChiTieu
{
    public partial class FormQuanLyChiTieu : Form
    {
       
        Boolean flagLocNgay = false;
        List<ChiTieu> chiTieu = new List<ChiTieu>{
    new ChiTieu { Id = "1", Title = "Nhận lương tháng 3", Type = "Thu", Category = "Lương", Date = new DateTime(2025, 6, 1).ToString("dd/MM/yyyy"), Amount = "2000000VND" },
    new ChiTieu { Id = "2", Title = "Ăn sáng", Type = "Chi", Category = "Ăn uống", Date = new DateTime(2025, 6, 2).ToString("dd/MM/yyyy"), Amount = "30000VND" },
    new ChiTieu { Id = "3", Title = "Mua sách", Type = "Chi", Category = "Giáo dục", Date = new DateTime(2025, 6, 3).ToString("dd/MM/yyyy"), Amount = "120000VND" },
    new ChiTieu { Id = "4", Title = "Thưởng", Type = "Thu", Category = "Thưởng", Date = new DateTime(2025, 6, 5).ToString("dd/MM/yyyy"), Amount = "500000VND" },
    new ChiTieu { Id = "5", Title = "Cà phê", Type = "Chi", Category = "Ăn uống", Date = new DateTime(2025, 6, 6).ToString("dd/MM/yyyy"), Amount = "40000VND" }
};
        List<ChiTieu> dataShow = new List<ChiTieu>();

        
        public FormQuanLyChiTieu()
        {
            InitializeComponent();
        }

        void loadTableQuanLy(List<ChiTieu> data)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in data)
            {
                dataGridView1.Rows.Add(item.Id,item.Title, item.Type, item.Category, item.Date, item.Amount);
            }
        }

        void loadThongKeSoDu()
        {
            long tongThu = 0;
            long tongChi = 0;
            foreach (var item in chiTieu)
            {
                if (item.Type == "Thu")
                {
                    tongThu += long.Parse(item.Amount.Replace("VND", ""));
                }
                else
                {
                    tongChi += long.Parse(item.Amount.Replace("VND", ""));
                }
            }
            lblTongThuNhap.Text = "Tổng Thu: " + tongThu.ToString("N0") + " VND";
            lblTongChi.Text = "Tổng Chi: " + tongChi.ToString("N0") + " VND";
            lblSoDu.Text = "Số Dư: " + (tongThu - tongChi).ToString("N0") + " VND";
        }

        void resetForm()
        {
            txtName.Text = "";
            txtSoTien.Text = "";
            comboBoxLoaiGiaoDich.SelectedIndex = -1;
            dateTimePickerNgayGiaoDich.Value = DateTime.Now;
        }

        private void FormQuanLyChiTieu_Load(object sender, EventArgs e)
        {
            foreach (var item in chiTieu)
            {
                dataShow.Add(item);
            }

            loadTableQuanLy(dataShow);
            loadThongKeSoDu();
        }

        void copyData(List<ChiTieu> data)
        {
            dataShow.Clear();
            foreach (var item in data)
            {
                dataShow.Add(item);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddTransition_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text.Length == 0 || txtSoTien.Text.Length == 0 || comboBoxLoaiGiaoDich.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin giao dịch!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                long soTien = int.Parse(txtSoTien.Text);
                
                ChiTieu newChiTieu = new ChiTieu(DateTime.Now.ToString("HHmmss"),txtName.Text, comboBoxLoaiGiaoDich.SelectedItem.ToString(), "Test", dateTimePickerNgayGiaoDich.Value.ToString("dd/MM/yyyy"), soTien.ToString() + "VND");
                chiTieu.Add(newChiTieu);
                copyData(chiTieu);
                loadTableQuanLy(dataShow);
                loadThongKeSoDu();
            }
            catch (Exception error)
            {
                MessageBox.Show("Số tiền phải là số không được chứa ký tự chữ cái hoặc in hoa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void dataGridView1_CellContentClick_4(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["btnXoa"].Index)
            {
                DialogResult check = MessageBox.Show("Bạn có chắc xoá dòng này không\nNếu xoá dữ liệu không thể khôi phục", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(check.ToString() == "OK")
                {
                    int rowIndex = e.RowIndex;
                    var id = dataGridView1.Rows[rowIndex].Cells["thId"].Value;
                    chiTieu.RemoveAll(item => item.Id == id.ToString());
                    MessageBox.Show("Bạn vừa xoá sản phẩm có Id là " + id, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    copyData(chiTieu);
                    loadTableQuanLy(dataShow);
                    loadThongKeSoDu();
                }
            }

            if (e.ColumnIndex == dataGridView1.Columns["btnSua"].Index)
            {
                int rowIndex = e.RowIndex;

                // Lấy giá trị từng cột
                
                var id = dataGridView1.Rows[rowIndex].Cells["thId"].Value;
                var title = dataGridView1.Rows[rowIndex].Cells["thTen"].Value;
                var type = dataGridView1.Rows[rowIndex].Cells["thLoaiGiaoDich"].Value;
                var category = dataGridView1.Rows[rowIndex].Cells["thDanhMuc"].Value;
                var date = dataGridView1.Rows[rowIndex].Cells["thNgayGiaoDich"].Value;
                var amount = dataGridView1.Rows[rowIndex].Cells["thSoTien"].Value;

                grbThemGiaoDich.Text = "Sửa Giao Dịch";
                btnHuySua.Visible = true;
                txtName.Text = title.ToString();
                txtSoTien.Text = amount.ToString();
                txtId.Text = id.ToString();
                comboBoxLoaiGiaoDich.SelectedItem = type.ToString();
                dateTimePickerBoLocNgayGiaoDich.Value = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", null);

                buttonSuaGiaoDich.Visible = true;
            }
        }

        private void comboBoxBoLocLoai_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            handleFilterAndSeacrh(flagLocNgay);
        }

        List<ChiTieu> boLocLoaiGiaoDich(List<ChiTieu> data)
        {
            if(comboBoxBoLocLoai.SelectedItem != null && (comboBoxBoLocLoai.SelectedItem.ToString() == "Tất Cả" || comboBoxBoLocLoai.SelectedItem.ToString() == ""))
            {
                return data;
            }
            List<ChiTieu> dataLoc = new List<ChiTieu>();
            foreach (var item in data)
            {
                if (comboBoxBoLocLoai.SelectedItem != null && item.Type == comboBoxBoLocLoai.SelectedItem.ToString())
                {
                    dataLoc.Add(item);
                }
            }
            
            return dataLoc;
        }

        List<ChiTieu> boLocNgayGiaoDich(List<ChiTieu> data)
        {
            List<ChiTieu> dataLoc = new List<ChiTieu>();
            string dateLoc = dateTimePickerBoLocNgayGiaoDich.Value.ToString("dd/MM/yyyy");

            foreach (var item in data)
            {
                
                if (item.Date == dateLoc)
                {
                    dataLoc.Add(item);
                }
            }
            return dataLoc;
        }
        List<ChiTieu> timKiemTheoTen(List<ChiTieu> data)
        {
            List<ChiTieu> dataLoc = new List<ChiTieu>();
            if(txtTimKiem.Text == "")
            {
                return data;
            }
            foreach (var item in data)
            {
                if (item.Title.ToLower().Contains(txtTimKiem.Text.ToLower()))
                {
                    dataLoc.Add(item);
                }
            }
            return dataLoc;
        }
        void handleFilterAndSeacrh(Boolean flagLocNgay)
        {
            List<ChiTieu> dataShowNew = chiTieu;

            if (flagLocNgay)
            {
                dataShowNew = boLocNgayGiaoDich(dataShowNew);
            }

            dataShowNew = boLocLoaiGiaoDich(dataShowNew);
            foreach(var item in dataShowNew)
            {
                Console.WriteLine(item.Title);
            }

            dataShowNew = timKiemTheoTen(dataShowNew);

            copyData(dataShowNew);
            loadTableQuanLy(dataShow);
        }

        //}

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            handleFilterAndSeacrh(flagLocNgay);
        }
        
        private void dateTimePickerBoLocNgayGiaoDich_ValueChanged_1(object sender, EventArgs e)
        {
            flagLocNgay = true;
            handleFilterAndSeacrh(flagLocNgay);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            flagLocNgay = false;
            comboBoxBoLocLoai.SelectedIndex = 0;
            //comboBoxBoLocDanhMuc.SelectedIndex = -1;
            dateTimePickerBoLocNgayGiaoDich.Value = DateTime.Now;
            txtTimKiem.Text = "";
            copyData(chiTieu);
            loadTableQuanLy(chiTieu);
        }

        private void buttonSuaGiaoDich_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            var chiTieuSua = chiTieu.FirstOrDefault(item => item.Id == id);
            if(chiTieuSua != null)
            {
                chiTieuSua.Title = txtName.Text;
                chiTieuSua.Type = comboBoxLoaiGiaoDich.SelectedItem.ToString();
                chiTieuSua.Date = dateTimePickerNgayGiaoDich.Value.ToString("dd/MM/yyyy");
                chiTieuSua.Amount = txtSoTien.Text + "VND";
                copyData(chiTieu);
                loadTableQuanLy(dataShow);
                loadThongKeSoDu();
                grbThemGiaoDich.Text = "Thêm Giao Dịch";
                btnHuySua.Visible = false;
                buttonSuaGiaoDich.Visible = false;
                resetForm();
            }
        }

        private void btnHuySua_Click(object sender, EventArgs e)
        {
            copyData(chiTieu);
            loadTableQuanLy(dataShow);
            loadThongKeSoDu();
            grbThemGiaoDich.Text = "Thêm Giao Dịch";
            btnHuySua.Visible = false;
            buttonSuaGiaoDich.Visible = false;
            resetForm();
        }

        
        private void btnBieuDo_Click(object sender, EventArgs e)
        {
            ThongTinNguoiDung thongTinNguoiDung = new ThongTinNguoiDung();
            thongTinNguoiDung.Show();
            this.Hide();
        }

        private void btnNguoiDung_Click(object sender, EventArgs e)
        {
            ThongTinNguoiDung thongTinNguoiDung = new ThongTinNguoiDung();
            thongTinNguoiDung.Show();
            this.Hide();
        }
    }

    class ChiTieu
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }

        public ChiTieu()
        {

        }
        public ChiTieu(string Id, string title, string type, string category, string date, string amount)
        {
            this.Id = Id;
            Title = title;
            Type = type;
            Category = category;
            Date = date;
            Amount = amount;
        }
    }
}
