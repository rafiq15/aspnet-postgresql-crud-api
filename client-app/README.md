# Product Management System - React Frontend

This is the React frontend for the Product Management API built with ASP.NET Core.

## Features

- ✅ View all products in a responsive grid layout
- ✅ Create new products with validation
- ✅ Update existing products
- ✅ Delete products with confirmation
- ✅ Beautiful and modern UI
- ✅ Form validation matching API requirements
- ✅ Error handling and loading states

## Prerequisites

- Node.js (v14 or higher)
- npm or yarn
- Running ASP.NET Core API on http://localhost:5000

## Installation

1. Navigate to the client-app directory:
```bash
cd client-app
```

2. Install dependencies:
```bash
npm install
```

## Configuration

The API base URL is configured in `src/services/productService.js`:
```javascript
const API_BASE_URL = 'http://localhost:5000/api';
```

If your API runs on a different port, update this URL accordingly.

## Running the Application

1. Make sure your ASP.NET Core API is running on port 5000
2. Start the React development server:
```bash
npm start
```

The application will open in your browser at http://localhost:3000

## Project Structure

```
client-app/
├── public/
│   └── index.html
├── src/
│   ├── components/
│   │   ├── ProductList.js      # Main component displaying all products
│   │   ├── ProductList.css
│   │   ├── ProductForm.js      # Form for creating/editing products
│   │   └── ProductForm.css
│   ├── services/
│   │   └── productService.js   # API integration layer
│   ├── App.js                  # Root component
│   ├── App.css
│   ├── index.js               # Entry point
│   └── index.css
└── package.json
```

## API Endpoints Used

- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get a specific product
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

## Form Validation

The form validates according to API DTO requirements:
- **Name**: Required, max 100 characters
- **Description**: Required, max 500 characters
- **Price**: Required, must be between $0 and $500

## Build for Production

```bash
npm run build
```

This creates an optimized production build in the `build/` folder.

## Technologies Used

- React 18
- Axios for HTTP requests
- CSS3 for styling
- Modern ES6+ JavaScript

## Troubleshooting

### CORS Issues
Make sure your ASP.NET Core API has CORS enabled for http://localhost:3000. The API should have:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
```

### Connection Refused
Ensure your ASP.NET Core API is running before starting the React app.

### Port Already in Use
If port 3000 is already in use, React will prompt you to use another port.
