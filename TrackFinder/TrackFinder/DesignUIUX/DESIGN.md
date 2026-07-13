---
name: Nexus Blue
colors:
  surface: '#f8f9ff'
  surface-dim: '#cbdbf5'
  surface-bright: '#f8f9ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#eff4ff'
  surface-container: '#e5eeff'
  surface-container-high: '#dce9ff'
  surface-container-highest: '#d3e4fe'
  on-surface: '#0b1c30'
  on-surface-variant: '#3f4850'
  inverse-surface: '#213145'
  inverse-on-surface: '#eaf1ff'
  outline: '#6f7881'
  outline-variant: '#bfc7d2'
  surface-tint: '#006495'
  primary: '#006191'
  on-primary: '#ffffff'
  primary-container: '#007bb6'
  on-primary-container: '#fcfcff'
  inverse-primary: '#8fcdff'
  secondary: '#46617c'
  on-secondary: '#ffffff'
  secondary-container: '#c1ddfd'
  on-secondary-container: '#47617d'
  tertiary: '#445f6f'
  on-tertiary: '#ffffff'
  tertiary-container: '#5d7889'
  on-tertiary-container: '#fbfcff'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#cbe6ff'
  primary-fixed-dim: '#8fcdff'
  on-primary-fixed: '#001e30'
  on-primary-fixed-variant: '#004b71'
  secondary-fixed: '#cfe5ff'
  secondary-fixed-dim: '#aec9e9'
  on-secondary-fixed: '#001d34'
  on-secondary-fixed-variant: '#2e4963'
  tertiary-fixed: '#cae7fa'
  tertiary-fixed-dim: '#aecbdd'
  on-tertiary-fixed: '#001e2c'
  on-tertiary-fixed-variant: '#2f4a59'
  background: '#f8f9ff'
  on-background: '#0b1c30'
  surface-variant: '#d3e4fe'
typography:
  headline-xl:
    fontFamily: Hanken Grotesk
    fontSize: 48px
    fontWeight: '700'
    lineHeight: 56px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Hanken Grotesk
    fontSize: 32px
    fontWeight: '600'
    lineHeight: 40px
    letterSpacing: -0.01em
  headline-md:
    fontFamily: Hanken Grotesk
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  body-lg:
    fontFamily: Hanken Grotesk
    fontSize: 18px
    fontWeight: '400'
    lineHeight: 28px
  body-md:
    fontFamily: Hanken Grotesk
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  label-md:
    fontFamily: Geist
    fontSize: 14px
    fontWeight: '500'
    lineHeight: 20px
    letterSpacing: 0.01em
  code-sm:
    fontFamily: Geist
    fontSize: 13px
    fontWeight: '400'
    lineHeight: 20px
  headline-lg-mobile:
    fontFamily: Hanken Grotesk
    fontSize: 28px
    fontWeight: '600'
    lineHeight: 36px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base: 4px
  xs: 0.5rem
  sm: 1rem
  md: 1.5rem
  lg: 2rem
  xl: 3rem
  gutter: 24px
  margin-mobile: 16px
  margin-desktop: 48px
---

## Brand & Style

The design system is engineered for a high-performance e-learning environment tailored to Computer Science students. The brand personality is **Academic but Progressive**, blending the rigor of technical education with the fluidity of modern SaaS product design. 

The aesthetic follows a **Modern SaaS UI** movement, characterized by:
- **Clarity & Logic:** Information architecture prioritizes scannability and structural hierarchy to reduce cognitive load during complex learning tasks.
- **Precision:** Clean lines, ample whitespace, and consistent alignments reflect the systematic nature of coding and engineering.
- **Soft Modernity:** While the structure is rigid, the interface remains approachable through the use of generous corner radii and ambient, multi-layered shadows.

## Colors

This design system utilizes a monochromatic blue foundation to maintain focus and professionalism. 

- **Primary (#1F95D8):** The core action color, used for primary buttons, active states, and progress indicators.
- **Secondary (#0D2B44):** A deep, midnight blue used for high-contrast typography and side navigation backgrounds to provide structural grounding.
- **Tertiary (#C9E6F9):** A soft tint used for subtle highlights, such as badge backgrounds or hover states on light surfaces.
- **Neutrals:** A scale of slate grays is used for body text (#334155) and borders (#E2E8F0) to ensure high readability without the harshness of pure black.
- **Semantic Colors:** Utilize standard Success (Green), Warning (Amber), and Error (Red) sparingly, keeping them desaturated to match the cool blue palette.

## Typography

The typography strategy leverages **Hanken Grotesk** for its exceptional legibility and sharp, contemporary geometric forms. This font provides the professional "SaaS" feel required for a technical platform. 

For technical data, metadata, and actual code snippets, the system utilizes **Geist**. As a monospaced-influenced sans-serif, it provides the necessary precision for CS students while maintaining visual harmony with the primary typeface.

- **Scale:** Use a 1.25x (Major Third) scale for headings.
- **Hierarchy:** Reserve the boldest weights for headlines and the deepest blue (Secondary) for maximum contrast. 
- **Readability:** Body text should maintain a line length of 60-75 characters for optimal reading of educational content.

## Layout & Spacing

The layout utilizes a **12-column Fluid Grid** for desktop and a **4-column Fluid Grid** for mobile. 

- **Vertical Rhythm:** A strict 4px/8px baseline grid is used to align all components. All margins and paddings must be multiples of 4px.
- **Containerization:** Content is grouped into logical modules (cards or sections) with a standard `md` (24px) padding.
- **Reflow:** On tablet devices, the side navigation collapses into a rail or hamburger menu to prioritize the content area.
- **Density:** Maintain "Comfortable" density for learning modules and "Compact" density for data-heavy dashboards or grade lists.

## Elevation & Depth

Hierarchy is established through **Ambient Shadows** and **Tonal Layering** rather than heavy borders.

- **Low Elevation:** Used for cards and persistent surfaces. A subtle 1px border (#E2E8F0) paired with a very soft shadow: `0 2px 4px rgba(13, 43, 68, 0.05)`.
- **Mid Elevation:** Used for hover states on cards or dropdown menus. `0 10px 15px -3px rgba(13, 43, 68, 0.1)`.
- **High Elevation:** Reserved for modals and critical pop-overs. `0 20px 25px -5px rgba(13, 43, 68, 0.15)`.
- **Tonal Depth:** The main background uses the lightest tint (#F1F8FE), while content cards sit "above" on pure white (#FFFFFF) surfaces.

## Shapes

The shape language is consistently **Rounded**, providing a friendly counter-balance to the technical nature of Computer Science.

- **Standard (8px):** Buttons, input fields, and small UI widgets.
- **Large (16px):** Main content cards, modals, and featured banners.
- **Extra Large (24px):** Profile sections or large hero containers.
- **Interactive Elements:** Ensure focus states use a 2px offset ring in the primary color (#1F95D8) to maintain accessibility.

## Components

- **Buttons:** Primary buttons use a solid Primary Blue fill with white text. Secondary buttons use a Tertiary Blue background with Primary Blue text. All buttons have an 8px radius.
- **Input Fields:** Use a 1px border (#E2E8F0). On focus, the border shifts to Primary Blue with a subtle outer glow. Use `label-md` for field labels.
- **Chips:** Small, 4px rounded indicators for "Course Tags" or "Status." Use Primary Blue at 10% opacity for the background and 100% opacity for the text.
- **Lists:** Use horizontal dividers (#F1F5F9) between list items. Use 16px vertical padding for list rows to ensure touch-friendly targets.
- **Cards:** Cards are white with an 8px radius and the "Low Elevation" shadow. They should include a 4px top-border accent in Primary Blue for "Featured" or "Active" courses.
- **Progress Bars:** Use a thick 8px track in Tertiary Blue with a Primary Blue fill to indicate course completion.