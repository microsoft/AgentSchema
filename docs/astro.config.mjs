// @ts-check
import { defineConfig } from "astro/config";
import starlight from "@astrojs/starlight";
import mermaid from "astro-mermaid";

// <https://astro.build/config>
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
      customCss: [
        // Path to custom CSS file for modern theme
        "./src/styles/custom.css",
      ],
      social: [
        {
          icon: "github",
          label: "GitHub",
          href: "https://github.com/sethjuarez/AgentSchema",
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
          label: "Reference",
          autogenerate: {
            directory: "reference",
          },
        },
      ],
    }),
  ],
});
