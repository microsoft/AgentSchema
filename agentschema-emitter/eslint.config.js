// @ts-check
import eslint from "@eslint/js";
import tsEslint from "typescript-eslint";

export default tsEslint.config(
  {
    ignores: ["**/dist/**/*", "**/.temp/**/*"],
  },
  eslint.configs.recommended,
  ...tsEslint.configs.recommended,
  {
    rules: {
      "@typescript-eslint/no-explicit-any": "off",
      "@typescript-eslint/no-unused-vars": "off",
      "no-unused-vars": "off",
      "no-case-declarations": "off",
      "no-prototype-builtins": "off",
      "no-useless-escape": "off"
    }
  }
);
