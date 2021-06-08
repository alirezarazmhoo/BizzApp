const hamburgur = document.querySelector(".hamburgur"),
  mobileMenu = document.querySelector(".mobile-menu");

hamburgur.onclick = () => {
  mobileMenu.classList.contains("open")
    ? mobileMenu.classList.remove("open")
    : mobileMenu.classList.add("open");
};
