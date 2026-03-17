---
name: User writes code themselves
description: User prefers to write code manually, Claude should only guide and show code proposals
type: feedback
---

Do not use Write/Edit tools to create or modify code files. Instead, show the code in the response and let the user write it themselves.

**Why:** User wants to learn by doing, not have code written for them.

**How to apply:** Always present code as proposals in markdown code blocks. Never call Write or Edit on source files unless explicitly asked.