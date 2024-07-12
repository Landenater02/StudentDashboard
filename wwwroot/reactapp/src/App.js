import React, { useEffect, useState } from 'react';

const App = () => {
    const [visits, setVisits] = useState([]);

    useEffect(() => {
        fetch('/api/visits')
            .then(response => response.json())
            .then(data => setVisits(data))
            .catch(error => console.error('Error fetching data:', error));
    }, []);

    return (
        <div>
            <h1>Visits</h1>
            <ul>
                {visits.map(visit => (
                    <li key={visit.id}>
                        {visit.advisor} met with {visit.student} on {new Date(visit.date).toLocaleDateString()}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default App;
