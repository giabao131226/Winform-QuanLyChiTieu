using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShareddData;
namespace QuanLyChiTieu
{
    public partial class BangDuKien : Form
    {

        BangDuKienDL bangDuKienDL = new BangDuKienDL();

        public BangDuKien()
        {
            InitializeComponent();
        }


        void hienThiDanhMuc(string thang,string nam)
        {
            List<ShareddData.DanhMuc> danhMucs = bangDuKienDL.hienThiBangDuKien(thang, nam);
            BangTongHop.Items.Clear();
            for(int i = 0; i < danhMucs.Count; i++)
            {
                Button btnEdit = new Button();
                btnEdit.Text = "Sửa";
                Button btnRemove = new Button();
                btnRemove.Text = "Xóa";
                BangTongHop.Items.Add(new ListViewItem(new string[] { danhMucs[i].TenDanhMuc, danhMucs[i].TienDuKien.ToString(), "0", danhMucs[i].TienDuKien.ToString(), danhMucs[i].Id.ToString() }));
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string loaiChiTieu = comboBoxLoaiGiaoDich.SelectedItem.ToString();
            string tenDanhMuc = txtTenDanhMuc.Text;
            decimal soTien = txtTienDuKien.Text != "" ? decimal.Parse(txtTienDuKien.Text) : 0;
            string ngayTao = dateTimePickerNgayTao.Value.ToString("dd/MM/yyyy");

            if (loaiChiTieu == "")
            {
                lblLoaiGiaoDichError.Text = "Vui lòng chọn loại giao dịch.";
            }
            if (tenDanhMuc == "")
            {
                lblTenDanhMucError.Text = "Vui lòng nhập tên danh mục.";
            }
            if (soTien <= 0)
            {
                lblSoTienDuKienError.Text = "Vui lòng nhập số tiền dự kiến lớn hơn 0.";
            }

            string thang = ngayTao.Split('/')[1];
            string nam = ngayTao.Split('/')[2];
            // Kiểm tra đã có bảng dự kiến chi tiêu nào cho tháng và năm này chưa
            try
            {
                bool ok = bangDuKienDL.isTakenDuKien(thang, nam);
                if (!ok)
                {
                    MessageBox.Show("Chưa có bảng dự kiến chi tiêu nào cho tháng " + thang + " năm " + nam + ". Vui lòng tạo bảng dự kiến trước khi thêm danh mục chi tiêu.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra bảng dự kiến: " + ex.Message);
                return;

            }

            try
            {
                bool success = bangDuKienDL.addCategory(tenDanhMuc, loaiChiTieu, soTien, ngayTao);
                if (success)
                {
                    MessageBox.Show("Thêm danh mục chi tiêu dự kiến thành công!");
                    // Xóa các trường nhập liệu sau khi thêm thành công
                    comboBoxLoaiGiaoDich.SelectedIndex = -1;
                    txtTenDanhMuc.Clear();
                    txtTienDuKien.Clear();
                    dateTimePickerNgayTao.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("Thêm danh mục chi tiêu dự kiến thất bại. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm danh mục chi tiêu dự kiến: " + ex.Message);

            }
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            foreach(var item in BangTongHop.SelectedItems)
            {
                ListViewItem bien = (ListViewItem)item;
                bool ok = bangDuKienDL.removeCategory(int.Parse(bien.SubItems[4].Text));
                if (ok)
                {
                    BangTongHop.Items.Remove(bien);
                }
                else
                {
                    MessageBox.Show("Xóa danh mục chi tiêu dự kiến thất bại. Vui lòng thử lại.");
                }
            }
        }

        private void btnTaoBangDuKien_Click(object sender, EventArgs e)
        {
            string thang = comboBoxMonth.SelectedItem.ToString().Split(' ')[1];
            string nam = txtNam.Text.ToString();
            if (nam == "" || thang == "")
            {
                MessageBox.Show("Vui lòng chọn tháng và năm để tạo bảng dự kiến.");
                return;
            }
            try
            {
                bool ok = bangDuKienDL.isTakenDuKien(thang, nam);
                if (ok)
                {
                    MessageBox.Show("Đã tồn tại bảng dự kiến chi tiêu cho tháng " + thang + " năm " + nam + ". Vui lòng chọn tháng và năm khác để tạo bảng dự kiến.");
                    return;
                }
                else
                {
                    bool success = bangDuKienDL.createBangDuKien(thang, nam);
                    if (success)
                    {
                        MessageBox.Show("Tạo bảng dự kiến chi tiêu thành công cho tháng " + thang + " năm " + nam + "!");
                        lblTieuDeBangDuKien.Text = "Bảng dự kiến chi tiêu tháng " + thang + " năm " + nam;
                        hienThiDanhMuc(thang, nam);
                    }
                    else
                    {
                        MessageBox.Show("Tạo bảng dự kiến chi tiêu thất bại. Vui lòng thử lại.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo bảng dự kiến chi tiêu: " + ex.Message);
            }
        }
        void hienThiBangDuKien()
        {
            string thang = comboBoxMonth.SelectedItem.ToString().Split(' ')[1];
            string nam = txtNam.Text.ToString();


            if (nam == "" || thang == "")
            {
                MessageBox.Show("Vui lòng chọn tháng và năm để hiển thị bảng dự kiến.");
                return;
            }

            try
            {
                bool ok = bangDuKienDL.isTakenDuKien(thang, nam);
                Console.WriteLine(ok);
                if (ok)
                {
                    lblTieuDeBangDuKien.Text = "Bảng dự kiến chi tiêu tháng " + thang + " năm " + nam;
                    hienThiDanhMuc(thang, nam);
                }
                else
                {
                    lblTieuDeBangDuKien.Text = "Chưa có bảng dự kiến chi tiêu nào cho tháng " + thang + " năm " + nam;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị bảng dự kiến: " + ex.Message);

            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            hienThiBangDuKien();
        }

        private void btnNguoiDung_Click(object sender, EventArgs e)
        {
            ThongTinNguoiDung thongTinNguoiDung = new ThongTinNguoiDung();
            thongTinNguoiDung.Show();
            this.Hide();
        }

        private void btnViTien_Click(object sender, EventArgs e)
        {
            FormQuanLyChiTieu quanLyChiTieu = new FormQuanLyChiTieu();
            quanLyChiTieu.Show();
            this.Close();

        }
    }
    class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public DateTime NgayTao { get; set; }
        public string Loai { get; set; }
        public decimal TienDuKien { get; set; }
        public int? IDGiaoDich { get; set; }
        public int IDBangDuKien { get; set; }
    }
}
