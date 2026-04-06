import { Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import UserFormPage from './pages/UserFormPage';
import UsersPage from './pages/UsersPage';

const AppLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <main className="max-w-7xl mx-auto px-4 py-8">
        {children}
      </main>
    </div>
  );
};

function App() {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <AppLayout>
            <UsersPage />
          </AppLayout>
        }
      />
      <Route
        path="/users/new"
        element={
          <AppLayout>
            <UserFormPage />
          </AppLayout>
        }
      />
      <Route
        path="/users/:id/edit"
        element={
          <AppLayout>
            <UserFormPage />
          </AppLayout>
        }
      />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}

export default App;