// @ts-check
import { defineConfig } from "astro/config";
import starlight from "@astrojs/starlight";
import mermaid from "astro-mermaid";

// https://astro.build/config
export default defineConfig({
  site: "https://microsoft.github.io",
  base: "/AgentSchema",
  trailingSlash: "always",
  integrations: [
    mermaid({
      theme: "forest",
      autoTheme: true,
    }),
    starlight({
      title: "AgentSchema",
      description: "A modern specification for building agents with ease",
      // Disable pagefind on Windows ARM64 (not supported yet)
      // Search will still work on GitHub Pages deployment
      pagefind:
        process.platform === "win32" && process.arch === "arm64" ? false : true,
      customCss: [
        // Path to custom CSS file for modern theme
        "./src/styles/custom.css",
      ],
      social: [
        {
          icon: "github",
          label: "GitHub",
          href: "https://github.com/microsoft/AgentSchema",
        },
      ],
      sidebar: [
        { label: "Home", link: "/" },
        {
          label: "Getting Started",
          autogenerate: {
            directory: "guides",
          },
        },
        {
          label: "SDKs",
          autogenerate: {
            directory: "sdks",
          },
        },
        {
          label: "Reference",
          autogenerate: {
            directory: "reference",
          },
        },
      ],
    }),
  ],
});
