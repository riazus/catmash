import React, { useState, useEffect } from 'react';
import './Leaderboard.css';
import { fetchCatApi } from '../../api/Cat/Cat';

export default function Leaderboard() {
  const [cats, setCats] = useState([]);

  useEffect(() => {
    fetchCatApi()
      .then((data) => {
        setCats(data);
      })
      .catch((error) => {
        console.log(error);
      })
  }, []);

  const sortedCats = cats.slice().sort((a, b) => b.likeCount - a.likeCount);

  return (
    <div className="leaderboard-container">
      <div>
        <h1 className="leaderboard-header">Leaderboard</h1>
      <div> 
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Image</th>
              <th>Likes</th>
            </tr>
          </thead>
          <tbody>
            {sortedCats.map((cat) => (
              <tr key={cat.externalId}>
                <td>{cat.externalId}</td>
                <td>
                  <img src={cat.imageUrl} alt="Cat" className="cat-image" />
                </td>
                <td>{cat.likeCount}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      </div>
    </div>
  );
}
