// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', () => {

  /* ============================================================
     PORTAL SIDEBAR (Community page) – mobile hamburger toggle
     ============================================================ */
  const sidebarToggle  = document.getElementById('portalSidebarToggle');
  const portalSidebar  = document.getElementById('portalSidebar');
  const sidebarOverlay = document.getElementById('portalSidebarOverlay');

  if (sidebarToggle && portalSidebar && sidebarOverlay) {
    const openSidebar = () => {
      portalSidebar.classList.add('open');
      sidebarOverlay.classList.add('open');
      sidebarToggle.setAttribute('aria-expanded', 'true');
      document.body.style.overflow = 'hidden';
    };

    const closeSidebar = () => {
      portalSidebar.classList.remove('open');
      sidebarOverlay.classList.remove('open');
      sidebarToggle.setAttribute('aria-expanded', 'false');
      document.body.style.overflow = '';
    };

    sidebarToggle.addEventListener('click', (e) => {
      e.stopPropagation();
      portalSidebar.classList.contains('open') ? closeSidebar() : openSidebar();
    });

    sidebarOverlay.addEventListener('click', closeSidebar);

    document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape') closeSidebar();
    });

    // Close the drawer automatically if a nav link is used to navigate away
    portalSidebar.querySelectorAll('a').forEach((link) => {
      link.addEventListener('click', () => {
        if (window.innerWidth <= 820) closeSidebar();
      });
    });

    window.addEventListener('resize', () => {
      if (window.innerWidth > 820) closeSidebar();
    });
  }

  /* ============================================================
     COMMENT REPLY TOGGLE
     Shows/hides the real MVC "Add a helpful reply..." form
     without altering its action, model binding, or antiforgery
     token. Purely a UI show/hide interaction.
     ============================================================ */
  document.querySelectorAll('[data-toggle-comment]').forEach((btn) => {
    btn.addEventListener('click', () => {
      const targetId = btn.getAttribute('data-toggle-comment');
      const form = document.getElementById(targetId);
      if (!form) return;

      const isCollapsed = form.classList.contains('is-collapsed');
      form.classList.toggle('is-collapsed');
      btn.setAttribute('aria-expanded', isCollapsed ? 'true' : 'false');

      if (isCollapsed) {
        const input = form.querySelector('input[name="Content"]');
        if (input) input.focus();
      }
    });
  });

  /* ============================================================
     SHARE BUTTON – copies the current page link to clipboard
     (purely client-side UX affordance, no backend call)
     ============================================================ */
  document.querySelectorAll('[data-share-post]').forEach((btn) => {
    btn.addEventListener('click', () => {
      const url = window.location.href;
      if (navigator.clipboard && navigator.clipboard.writeText) {
        navigator.clipboard.writeText(url).catch(() => {
          /* clipboard write failed silently; no-op fallback */
        });
      }
    });
  });

});
/* ===========================
   Custom Post Dropdown
   =========================== */

document.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll(".post-menu__trigger").forEach(btn => {

        btn.addEventListener("click", function (e) {
            e.preventDefault();
            e.stopPropagation();

            const menu = this.nextElementSibling;

            // اقفل أي قائمة تانية مفتوحة
            document.querySelectorAll(".post-menu__dropdown.show").forEach(drop => {
                if (drop !== menu) {
                    drop.classList.remove("show");
                }
            });

            // افتح/اقفل الحالية
            menu.classList.toggle("show");

            // حدث aria-expanded
            this.setAttribute(
                "aria-expanded",
                menu.classList.contains("show")
            );
        });

    });

    // اقفل القائمة عند الضغط خارجها
    document.addEventListener("click", () => {
        document.querySelectorAll(".post-menu__dropdown.show").forEach(menu => {
            menu.classList.remove("show");
        });

        document.querySelectorAll(".post-menu__trigger").forEach(btn => {
            btn.setAttribute("aria-expanded", "false");
        });
    });

});