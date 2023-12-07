document.addEventListener('DOMContentLoaded', function () {
    // Tìm nút "Thêm người dùng" và gán sự kiện click
    var btnThemNguoiDung = document.querySelector('.Them-TK');
    btnThemNguoiDung.addEventListener('click', function () {
        // Hiển thị overlay
        var overlay = document.createElement('div');
        overlay.setAttribute('class', 'overlay');
        document.body.appendChild(overlay);

        // Hiển thị popup-container
        var popupContainer = document.querySelector('.popup-container');
        popupContainer.style.display = 'block';

        // Thiết lập CSS cho overlay
        overlay.style.position = 'fixed';
        overlay.style.width = '100%';
        overlay.style.height = '100%';
        overlay.style.top = '0';
        overlay.style.left = '0';
        overlay.style.backgroundColor = 'rgba(0, 0, 0, 0.5)';
        overlay.style.zIndex = '999';

        // Đóng popup và overlay khi click vào overlay
        overlay.addEventListener('click', function () {
            popupContainer.style.display = 'none';
            document.body.removeChild(overlay);
        });
    });
});
