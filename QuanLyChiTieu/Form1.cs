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
using ShareddData;

namespace QuanLyChiTieu
{

    public partial class FormQuanLyChiTieu : Form
    {
        Boolean flagLocNgay = false;
        List<ShareddData.Transition> dataShow = new List<ShareddData.Transition>();
        List<ShareddData.Transition> chiTieu = new List<ShareddData.Transition>();
        List<ShareddData.DanhMuc> danhMucs = new List<ShareddData.DanhMuc>();
        CategoryService categoryService = new CategoryService();
        TransitionService transitionService = new TransitionService();

        public FormQuanLyChiTieu()
        {
            InitializeComponent();
        }

        void loadTableQuanLy(List<ShareddData.Transition> data)
        {
            dataGridView1.Rows.Clear();
            foreach (ShareddData.Transition t in data)
            {
                Console.WriteLine(t.TenGiaoDich);
                dataGridView1.Rows.Add(t.Id, t.TenGiaoDich, t.Loai, t.TenDanhMuc, t.NgayTao.ToString("dd/MM/yyyy"), t.SoTien.ToString() + " VND");
            }
        }
        void loadThongKeSoDu()
        {
            decimal tongThu = 0;
            decimal tongChi = 0;
            foreach (var item in chiTieu)
            {
                if (item.Loai == "Thu")
                {
                    tongThu += item.SoTien;
                }
                else
                {
                    tongChi += item.SoTien;
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
        void resetFilterAndSearch()
        {
            flagLocNgay = false;
            comboBoxBoLocLoai.SelectedIndex = 0;
            comboBoxBoLocDanhMuc.SelectedIndex = 0;
            txtTimKiem.Text = "";
        }
       
        private void FormQuanLyChiTieu_Load(object sender, EventArgs e)
        {

            string thang = dateTimePickerBoLocNgayGiaoDich.Value.Month.ToString();
            string nam = dateTimePickerBoLocNgayGiaoDich.Value.Year.ToString();
            try
            {
                string loai = comboBoxBoLocLoai.Text.ToString();
                if (loai == "Loại Giao Dịch" || loai == "Tất Cả") loai = "";
                List<ShareddData.DanhMuc> danhMucs = categoryService.hienThiDanhMuc(loai, thang, nam);
                comboBoxBoLocDanhMuc.Items.Clear();
                comboBoxBoLocDanhMuc.Items.Add("Tất Cả");
                foreach (var item in danhMucs)
                {
                    comboBoxBoLocDanhMuc.Items.Add(item.TenDanhMuc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load comboBox danh mục " + ex.Message);
            }
            try
            {
                List<ShareddData.Transition> transitions = transitionService.loadTransition("","", "", "");
                foreach(var item in transitions)
                {
                    chiTieu.Add(item);
                }
                copyData(chiTieu);
                loadTableQuanLy(chiTieu);
                loadThongKeSoDu();

            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi khi tải giao dịch: " + error.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        void copyData(List<ShareddData.Transition> data)
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

            if (txtName.Text.Length == 0 || comboBoxLoaiGiaoDich.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin giao dịch!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtSoTien.Text.Length == 0) txtSoTien.Text = "0";
            try
            {
                
                string tenGiaoDich = txtName.Text;
                string loai = comboBoxLoaiGiaoDich.SelectedItem.ToString();
                string ngayTao = dateTimePickerNgayGiaoDich.Value.ToString("dd/MM/yyyy");
                string danhMuc = comboBoxDanhMuc.SelectedItem != null ? comboBoxDanhMuc.SelectedItem.ToString() : "";
                decimal soTien = decimal.Parse(txtSoTien.Text);
                
                try
                {
                    bool ok = transitionService.addTransition(tenGiaoDich, loai, soTien, ngayTao, danhMuc);
                    if (ok)
                    {
                        MessageBox.Show("Thêm giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        chiTieu = transitionService.loadTransition("","","","");
                        foreach(var item in chiTieu)
                        {
                            Console.WriteLine(item.TenGiaoDich);
                        }
                        copyData(chiTieu);
                        Console.WriteLine("Số bản ghi sau khi load lại: " + dataShow.Count());
                        loadTableQuanLy(dataShow);
                        loadThongKeSoDu();
                        resetForm();
                        resetFilterAndSearch();
                    }
                    else
                    {
                        MessageBox.Show("Thêm giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Lỗi khi thêm giao dịch: " + error.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Số tiền phải là một số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi không xác định: " + error.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    int id = int.Parse(dataGridView1.Rows[rowIndex].Cells["thId"].Value.ToString());
                    string tenDanhMuc = dataGridView1.Rows[rowIndex].Cells["thDanhMuc"].Value.ToString();
                    try
                    {
                        bool ok = transitionService.removeTransition(id,tenDanhMuc);
                        if (ok)
                        {
                            MessageBox.Show("Bạn vừa xoá sản phẩm có Id là " + id, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            List<ShareddData.Transition> newList = new List<ShareddData.Transition>();
                            foreach(var item in chiTieu)
                            {
                                if(item.Id != id) newList.Add(item); 
                            }
                            chiTieu = newList;
                            copyData(newList);
                            loadTableQuanLy(dataShow);
                            loadThongKeSoDu();
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }
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
                var amount = dataGridView1.Rows[rowIndex].Cells["thSoTien"].Value.ToString().Split(' ')[0].Split('.')[0];

                grbThemGiaoDich.Text = "Sửa Giao Dịch";
                btnHuySua.Visible = true;
                txtName.Text = title.ToString();
                txtSoTien.Text = amount.ToString();
                txtId.Text = id.ToString();
                comboBoxLoaiGiaoDich.SelectedItem = type.ToString();
                comboBoxBoLocDanhMuc.SelectedItem = category.ToString();
                dateTimePickerNgayGiaoDich.Value = Convert.ToDateTime(date);
                buttonSuaGiaoDich.Visible = true;
            }
        }

        private void comboBoxBoLocLoai_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                string thang = dateTimePickerBoLocNgayGiaoDich.Value.Month.ToString();
                string nam = dateTimePickerBoLocNgayGiaoDich.Value.Year.ToString();
                string loai = comboBoxBoLocLoai.Text.ToString();
                if (loai == "Loại Giao Dịch" || loai == "Tất Cả") loai = "";
                List<ShareddData.DanhMuc> danhMucs = categoryService.hienThiDanhMuc(loai, thang, nam);
                comboBoxBoLocDanhMuc.Items.Clear();
                comboBoxBoLocDanhMuc.Items.Add("Tất Cả");
                foreach (var item in danhMucs)
                {
                    comboBoxBoLocDanhMuc.Items.Add(item.TenDanhMuc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load comboBox danh mục " + ex.Message);
            }
            handleFilterAndSeacrh(flagLocNgay);
        }

        List<ShareddData.Transition> boLocLoaiGiaoDich(List<ShareddData.Transition> data)
        {
            if(comboBoxBoLocLoai.SelectedItem != null && (comboBoxBoLocLoai.SelectedItem.ToString() == "Tất Cả" || comboBoxBoLocLoai.SelectedItem.ToString() == "" || comboBoxBoLocLoai.SelectedItem.ToString() == "Loại Giao Dịch"))
            {
                return data;
            }
            List<ShareddData.Transition> dataLoc = new List<ShareddData.Transition>();
            foreach (var item in data)
            {
                if (comboBoxBoLocLoai.SelectedItem != null && item.Loai == comboBoxBoLocLoai.SelectedItem.ToString())
                {
                    dataLoc.Add(item);
                }
            }
            
            return dataLoc;
        }

        List<ShareddData.Transition> boLocNgayGiaoDich(List<ShareddData.Transition> data)
        {
            List<ShareddData.Transition> dataLoc = new List<ShareddData.Transition>();
            string dateLoc = dateTimePickerBoLocNgayGiaoDich.Value.ToString("dd/MM/yyyy");

            foreach (var item in data)
            {
                if (item.NgayTao.ToString("dd/MM/yyyy") == dateLoc)
                    dataLoc.Add(item);
            }
               
            return dataLoc;
        }
        List<ShareddData.Transition> timKiemTheoTen(List<ShareddData.Transition> data)
        {
            List<ShareddData.Transition> dataLoc = new List<ShareddData.Transition>();
            if(txtTimKiem.Text == "")
            {
                return data;
            }
            foreach (var item in data)
            {
                if (item.TenGiaoDich.ToLower().Contains(txtTimKiem.Text.ToLower()))
                {
                    dataLoc.Add(item);
                }
            }
            return dataLoc;
        }

        List<ShareddData.Transition> boLocDanhMuc(List<ShareddData.Transition> data)
        {
            List<ShareddData.Transition> dataLoc = new List<ShareddData.Transition>();
            if(comboBoxBoLocDanhMuc.SelectedItem == null || comboBoxBoLocDanhMuc.SelectedItem.ToString() == "Tất Cả")
            {
                return data;
            }
            foreach (var item in data)
            {
                if (item.TenDanhMuc.ToLower().Contains(comboBoxBoLocDanhMuc.SelectedItem.ToString().ToLower())){
                    dataLoc.Add(item);
                }
            }
            return dataLoc;
        }

        void handleFilterAndSeacrh(Boolean flagLocNgay)
        {
            List<ShareddData.Transition> dataShowNew = chiTieu;

            if (flagLocNgay)
            {
                dataShowNew = boLocNgayGiaoDich(dataShowNew);
               
            }

            dataShowNew = boLocLoaiGiaoDich(dataShowNew);

            dataShowNew = boLocDanhMuc(dataShowNew);

            dataShowNew = timKiemTheoTen(dataShowNew);

            copyData(dataShowNew);
            loadTableQuanLy(dataShow);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            handleFilterAndSeacrh(flagLocNgay);
        }
        
        private void dateTimePickerBoLocNgayGiaoDich_ValueChanged_1(object sender, EventArgs e)
        {
            flagLocNgay = true;
            string thang = dateTimePickerBoLocNgayGiaoDich.Value.ToString("dd/MM/yyyy").Split('/')[1];
            string nam = dateTimePickerBoLocNgayGiaoDich.Value.ToString("dd/MM/yyyy").Split('/')[2];
            try
            {
                chiTieu = transitionService.loadTransition(thang, nam, "", "");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load lại dữ liệu thay đổi ngày: " + ex.Message);
            }
            handleFilterAndSeacrh(flagLocNgay);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            flagLocNgay = false;
            comboBoxBoLocLoai.SelectedIndex = 0;
            comboBoxBoLocDanhMuc.SelectedIndex = 0;
            dateTimePickerBoLocNgayGiaoDich.Value = DateTime.Now;
            txtTimKiem.Text = "";
            copyData(chiTieu);
            loadTableQuanLy(chiTieu);
        }

        private void buttonSuaGiaoDich_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string tenGiaoDich = txtName.Text;
                string loaiGiaoDich = comboBoxLoaiGiaoDich.SelectedItem.ToString();
                string ngayTao = dateTimePickerNgayGiaoDich.Value.ToString("dd/MM/yyyy");
                decimal soTien = decimal.Parse(txtSoTien.Text);
                string tenDanhMuc = comboBoxDanhMuc.Text;

                bool ok = transitionService.editTransition(id, tenGiaoDich, loaiGiaoDich, soTien, ngayTao, tenDanhMuc);

                if (ok)
                {
                    chiTieu = transitionService.loadTransition("", "", "", "");
                    copyData(chiTieu);
                    loadTableQuanLy(dataShow);
                    loadThongKeSoDu();
                    grbThemGiaoDich.Text = "Thêm Giao Dịch";
                    btnHuySua.Visible = false;
                    buttonSuaGiaoDich.Visible = false;
                    resetForm();
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật " + ex.Message);
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
            BangDuKien bangDuKien = new BangDuKien();
            this.Hide();
            bangDuKien.Show();
        }

        private void btnNguoiDung_Click(object sender, EventArgs e)
        {
            ThongTinNguoiDung thongTinNguoiDung = new ThongTinNguoiDung();
            thongTinNguoiDung.Show();
            this.Hide();
        }

        void loadDanhMuc()
        {
            if(comboBoxLoaiGiaoDich.SelectedItem == null)
            {
                return;
            }
            string thang = dateTimePickerNgayGiaoDich.Value.Month.ToString();
            string nam = dateTimePickerNgayGiaoDich.Value.Year.ToString();
            string selectedType = comboBoxLoaiGiaoDich.SelectedItem.ToString();
            try
            {
                danhMucs = categoryService.hienThiDanhMuc(selectedType, thang, nam);
                comboBoxDanhMuc.Items.Clear();
                foreach (var item in danhMucs)
                {
                    comboBoxDanhMuc.Items.Add(item.TenDanhMuc);
                }
            }catch(Exception error)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + error.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBoxLoaiGiaoDich_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            loadDanhMuc();
        }

        private void comboBoxBoLocDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            handleFilterAndSeacrh(flagLocNgay);
        }
       
        private void dateTimePickerNgayGiaoDich_ValueChanged(object sender, EventArgs e)
        {
            loadDanhMuc();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
