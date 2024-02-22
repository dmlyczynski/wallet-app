import './App.css';
import WalletBalanceList from './WalletBalanceList/WalletBalanceList';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


const App = () => {
  return (
    <div className="App">
      <h1>My Dashboard</h1>
      <header className="App-header">
        <WalletBalanceList />
        <ToastContainer />
      </header>
    </div>
  );
}

export default App;


