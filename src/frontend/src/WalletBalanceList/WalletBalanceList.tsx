import { useState, useEffect } from 'react';
import restApiClient from '../RestApiClient';
import WalletResponse from '../Models/WalletBalanceResponse';
import './WalletBalanceList.css';
import { toast } from 'react-toastify';

type WalletId = Pick<WalletResponse, "id">;

const WalletBalanceList = () => {
  const [wallets, setWalletBalances] = useState<WalletResponse[]>();
  const [amount, setAmount] = useState('');

  const [walletIds, setWalletIds] = useState<number[]>();

  const [selectedWalletId, setSelectedWalletId] = useState('');

  useEffect(() => {
    fetchWallets();
  }, []);

  const handleDeposit = async () => {
    try {
      await restApiClient.put('/wallet/addFunds?walletId='+selectedWalletId+'&funds='+amount+'');
      fetchWallets();
      setAmount('');
    } catch (error) {
      toast('Error add funds:'+ error);
      console.error('Error add funds:', error);
    }
  };

  const handleCreate = async () => {
    try {
      await restApiClient.post('/wallet/create');
      fetchWallets();
      setAmount('');
      toast("Wallet created!");
    } catch (error) {
      toast('Error create wallet:'+ error);
      console.error('Error create wallet:', error);
    }
  };

  const fetchWallets = async () => {
    try {
      const response = await restApiClient.get<WalletResponse[]>('/wallet/all');
      setWalletBalances(response.data);
      setWalletIds(response.data.map(x=> x.id));
    } catch (error) {
      toast('Error fetching wallet balances:'+ error);
      console.error('Error fetching wallet balances:', error);
    }
  };

  const handleWithdraw = async () => {
    try {
      await restApiClient.put('/wallet/removeFunds?walletId='+selectedWalletId+'&funds='+amount+'');
      fetchWallets();
      setAmount('');
    } catch (error) {
      toast('Error remove funds:'+ error);
      console.error('Error remove funds:', error);
    }
  };

  return (
    <div>
      <div className="operation-section">
        <h2>Perform Operations</h2>
        <div className="select-container">
        <select value={selectedWalletId} onChange={(e) => setSelectedWalletId(e.target.value)}>
          <option value="">Select account ID</option>
          {walletIds?.map(accountId => (
            <option key={accountId} value={accountId}>{accountId}</option>
          ))}
        </select>
        </div>
        <input
          type="number"
          placeholder="Amount"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
        />
        <button className="create-btn" onClick={handleCreate}>Create wallet</button>
        <button className="deposit-btn" onClick={handleDeposit}>Add funds</button>
        <button className="withdraw-btn" onClick={handleWithdraw}>Remove funds</button>
      </div>
      <h1>Wallet Balances</h1>
      <ul>
        {wallets?.map((wallet, index) => (
          <li key={index}>Account {index + 1}: {wallet.name}: Balance : {wallet.balance}</li>
        ))}
      </ul>
      
    
    </div>
    
  );
};

export default WalletBalanceList;
