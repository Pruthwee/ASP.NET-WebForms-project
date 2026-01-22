# Razor Pages Routes Reference

## Base URL
Assuming the application runs on: `https://localhost:5001` (or your configured port)

---

## Films Routes

### Index (List All Films)
- **URL**: `/Films` or `/Films/Index`
- **Method**: GET
- **Query Parameters**:
  - `searchString` - Filter by title
  - `sortOrder` - Sort order (title_asc, title_desc, year_asc, year_desc)
  - `pageNumber` - Page number for pagination
- **Example**: `/Films?searchString=Matrix&sortOrder=year_desc&pageNumber=1`

### Create Film
- **URL**: `/Films/Create`
- **Methods**: GET (show form), POST (submit form)

### Film Details
- **URL**: `/Films/Details?id={filmId}`
- **Method**: GET
- **Example**: `/Films/Details?id=123`

### Edit Film
- **URL**: `/Films/Edit?id={filmId}`
- **Methods**: GET (show form), POST (submit changes)
- **Example**: `/Films/Edit?id=123`

### Delete Film
- **URL**: `/Films/Delete?id={filmId}`
- **Methods**: GET (show confirmation), POST (confirm deletion)
- **Example**: `/Films/Delete?id=123`

---

## Actors Routes

### Index (List All Actors)
- **URL**: `/Actors` or `/Actors/Index`
- **Method**: GET
- **Query Parameters**:
  - `searchString` - Filter by name
  - `sortOrder` - Sort order (firstname_asc, firstname_desc, lastname_asc, lastname_desc)
  - `pageNumber` - Page number for pagination
- **Example**: `/Actors?searchString=Smith&sortOrder=lastname_asc&pageNumber=1`

### Create Actor
- **URL**: `/Actors/Create`
- **Methods**: GET (show form), POST (submit form)

### Actor Details
- **URL**: `/Actors/Details?id={actorId}`
- **Method**: GET
- **Example**: `/Actors/Details?id=456`

### Edit Actor
- **URL**: `/Actors/Edit?id={actorId}`
- **Methods**: GET (show form), POST (submit changes)
- **Example**: `/Actors/Edit?id=456`

### Delete Actor
- **URL**: `/Actors/Delete?id={actorId}`
- **Methods**: GET (show confirmation), POST (confirm deletion)
- **Example**: `/Actors/Delete?id=456`

---

## Directors Routes

### Index (List All Directors)
- **URL**: `/Directors` or `/Directors/Index`
- **Method**: GET
- **Query Parameters**:
  - `searchString` - Filter by name
  - `sortOrder` - Sort order (firstname_asc, firstname_desc, lastname_asc, lastname_desc)
  - `pageNumber` - Page number for pagination
- **Example**: `/Directors?searchString=Nolan&sortOrder=lastname_asc&pageNumber=1`

### Create Director
- **URL**: `/Directors/Create`
- **Methods**: GET (show form), POST (submit form)

### Director Details
- **URL**: `/Directors/Details?id={directorId}`
- **Method**: GET
- **Example**: `/Directors/Details?id=789`

### Edit Director
- **URL**: `/Directors/Edit?id={directorId}`
- **Methods**: GET (show form), POST (submit changes)
- **Example**: `/Directors/Edit?id=789`

### Delete Director
- **URL**: `/Directors/Delete?id={directorId}`
- **Methods**: GET (show confirmation), POST (confirm deletion)
- **Example**: `/Directors/Delete?id=789`

---

## Account Routes

### Login
- **URL**: `/Account/Login`
- **Methods**: GET (show form), POST (submit credentials)
- **Query Parameters**:
  - `returnUrl` - URL to redirect after successful login
- **Example**: `/Account/Login?returnUrl=/Films`

### Register
- **URL**: `/Account/Register`
- **Methods**: GET (show form), POST (submit registration)
- **Query Parameters**:
  - `returnUrl` - URL to redirect after successful registration
- **Example**: `/Account/Register?returnUrl=/Films`

### Logout
- **URL**: `/Account/Logout`
- **Methods**: GET, POST
- **Query Parameters**:
  - `returnUrl` - URL to redirect after logout
- **Example**: `/Account/Logout?returnUrl=/`

---

## Navigation Examples

### Linking to Pages in Razor
```html
<!-- Link to Films Index -->
<a asp-page="/Films/Index">View All Films</a>

<!-- Link to Create Film -->
<a asp-page="/Films/Create" class="btn btn-primary">Add New Film</a>

<!-- Link to Film Details with ID -->
<a asp-page="/Films/Details" asp-route-id="@filmId">View Details</a>

<!-- Link to Edit Film with ID -->
<a asp-page="/Films/Edit" asp-route-id="@filmId">Edit</a>

<!-- Link to Login with Return URL -->
<a asp-page="/Account/Login" asp-route-returnUrl="@Url.Page("/Films/Create")">Login</a>
```

### Redirecting in Code-Behind
```csharp
// Redirect to Index
return RedirectToPage("./Index");

// Redirect to Details with ID
return RedirectToPage("./Details", new { id = filmId });

// Redirect to different area
return RedirectToPage("/Films/Index");

// Local redirect (safe)
return LocalRedirect(returnUrl);
```

---

## URL Patterns Summary

| Entity    | Pattern                    | Example                 |
|-----------|----------------------------|-------------------------|
| Films     | `/Films/{action}?id={id}`  | `/Films/Edit?id=1`      |
| Actors    | `/Actors/{action}?id={id}` | `/Actors/Details?id=5`  |
| Directors | `/Directors/{action}?id={id}` | `/Directors/Delete?id=3` |
| Account   | `/Account/{action}?returnUrl={url}` | `/Account/Login?returnUrl=/Films` |

---

## HTTP Methods

| Action  | GET (View) | POST (Action) |
|---------|-----------|---------------|
| Index   | ✓         | -             |
| Create  | ✓ (Form)  | ✓ (Submit)    |
| Details | ✓         | -             |
| Edit    | ✓ (Form)  | ✓ (Submit)    |
| Delete  | ✓ (Confirm) | ✓ (Submit)  |
| Login   | ✓ (Form)  | ✓ (Submit)    |
| Register| ✓ (Form)  | ✓ (Submit)    |
| Logout  | ✓         | ✓             |

---

## Common Query Parameters

- **id**: Entity identifier (integer)
- **searchString**: Text search filter
- **sortOrder**: Column and direction for sorting
- **pageNumber**: Page number for pagination (1-based)
- **currentFilter**: Maintains filter during pagination
- **returnUrl**: URL to return after authentication

---

## Status Codes

- **200 OK**: Successful GET request
- **302 Found**: Redirect after successful POST
- **404 Not Found**: Entity not found
- **400 Bad Request**: Invalid input
- **500 Internal Server Error**: Server error

---

**Note**: All routes are relative to the application base URL. If authentication is required, unauthenticated users will be redirected to `/Account/Login` with the original URL as the `returnUrl` parameter.
