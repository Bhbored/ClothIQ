window.toggleCustomDropdown = (dropdownElement, menuElement) => {
    const toggleButton = dropdownElement.querySelector('.custom-dropdown-toggle');

    if (!toggleButton || !menuElement) {
        console.warn('Dropdown elements not found for toggleCustomDropdown');
        return;
    }

    document.querySelectorAll('.custom-dropdown-menu.show').forEach(openMenu => {
        if (openMenu !== menuElement) {
            openMenu.classList.remove('show');
            openMenu.previousElementSibling?.classList.remove('show'); 
        }
    });

    menuElement.classList.toggle('show');
    toggleButton.classList.toggle('show');

    const closeDropdown = (e) => {
        if (!dropdownElement.contains(e.target)) {
            menuElement.classList.remove('show');
            toggleButton.classList.remove('show');
            document.removeEventListener('click', closeDropdown); 
        }
    };

    if (menuElement.classList.contains('show')) {
        document.addEventListener('click', closeDropdown);
    } else {
        document.removeEventListener('click', closeDropdown); 
    }
};

window.closeCustomDropdown = (dropdownElementRef, menuElementRef) => {
    const dropdownElement = dropdownElementRef;
    const menuElement = menuElementRef;
    const toggleButton = dropdownElement.querySelector('.custom-dropdown-toggle');

    if (menuElement.classList.contains('show')) {
        menuElement.classList.remove('show');
        toggleButton.classList.remove('show');

    }
};