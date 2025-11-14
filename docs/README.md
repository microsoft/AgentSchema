# AgentSchema Documentation Site

This is the documentation site for AgentSchema, built with [Astro](https://astro.build) and [Starlight](https://starlight.astro.build/).

## Modern Theme

The site uses a custom modern theme with the following features:

### Design Elements

- **Purple/Blue gradient color scheme** - Professional and modern appearance
- **Glass morphism effects** - Cards with backdrop blur and subtle transparency
- **Smooth animations** - Hover effects and micro-interactions
- **Modern typography** - Clean system fonts with improved readability
- **Enhanced shadows** - Layered shadow system for depth

### Custom Hero Image

The site features a custom SVG hero image (`agentschema-hero.svg`) that visualizes:

- Connected agent network
- Data flow between agents
- Schema representation
- YAML configuration
- Animated particles and data packets

### Color Palette

- **Primary Accent**: `#6366f1` (Indigo)
- **Background Gradient**: `#667eea` to `#764ba2`
- **Gray Scale**: Tailwind-inspired neutral grays
- **Support Colors**: Cyan and emerald for data flow

### Styling Features

- Responsive design with mobile-first approach
- Dark mode support with adjusted color schemes
- Enhanced accessibility with proper focus states
- Modern card layouts with hover effects
- Improved code block styling
- Glass morphism navigation

## Development

To run the documentation site locally:

```bash
cd docs
npm install
npm run dev
```

The site will be available at `http://localhost:4321/`

## Building

To build the site for production:

```bash
npm run build
```

## Customization

The custom theme is located in `src/styles/custom.css` and can be modified to adjust:

- Color schemes
- Typography
- Spacing
- Animations
- Component styling

The Astro configuration is in `astro.config.mjs` where you can:

- Update site metadata
- Modify navigation structure
- Add integrations
- Configure build options
