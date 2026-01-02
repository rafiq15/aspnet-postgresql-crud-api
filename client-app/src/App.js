import React from 'react';
import ProductList from './components/ProductList';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Product Management System</h1>
        <p>Manage your products with ease</p>
      </header>
      <main>
        <ProductList />
      </main>
      <footer className="App-footer">
        <p>&copy; 2025 Product Management System. Powered by ASP.NET Core & React</p>
      </footer>
    </div>
  );
}

export default App;
