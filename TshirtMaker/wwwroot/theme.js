window.themeToggle = {
  toggle: function () {
    const html = document.documentElement;
    const currentTheme = html.getAttribute("data-custom-theme");
    const newTheme = currentTheme === "light" ? "dark" : "light";
    html.setAttribute("data-custom-theme", newTheme);
  },

  getCurrentTheme: function () {
    return document.documentElement.getAttribute("data-custom-theme");
  },
};

window.getScrollInfo = function () {
  const scrollPosition = window.innerHeight + window.scrollY;
  const threshold = document.documentElement.scrollHeight - 500;
  return {
    isNearBottom: scrollPosition >= threshold,
  };
};

window.scrollToElement = function (elementId) {
  const element = document.getElementById(elementId);
  if (!element) {
    console.warn("Element not found:", elementId);
    return;
  }

  const headerOffset = 80;
  const elementPosition = element.getBoundingClientRect().top;
  const offsetPosition = elementPosition + window.scrollY - headerOffset;

  window.scrollTo({
    top: Math.max(0, offsetPosition),
    behavior: "smooth",
  });
};

window.initScrollAnimations = function () {
  const selector =
    ".how-it-works-section, .describe-vision-section, .save-share-section, #features";
  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add("visible");
        }
      });
    },
    {
      threshold: 0.1,
      rootMargin: "0px 0px -50px 0px",
    },
  );

  function observeElement(el) {
    if (!el || el.nodeType !== 1 || el.dataset.scrollObserved === "1") return;
    el.dataset.scrollObserved = "1";
    observer.observe(el);
  }

  document.querySelectorAll(selector).forEach(observeElement);

  const mo = new MutationObserver((mutations) => {
    for (const m of mutations) {
      for (const node of m.addedNodes) {
        if (node.nodeType !== 1) continue;
        if (node.matches && node.matches(selector)) observeElement(node);
        if (node.querySelectorAll)
          node.querySelectorAll(selector).forEach(observeElement);
      }
    }
  });
  mo.observe(document.body, { childList: true, subtree: true });
};

if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", window.initScrollAnimations);
} else {
  window.initScrollAnimations();
}
