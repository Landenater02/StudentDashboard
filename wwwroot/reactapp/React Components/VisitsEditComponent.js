import React, { useState } from 'react';
import ReactDOM from 'react-dom/client';

const VisitsEditComponent = () => {
    const [advisor, setAdvisor] = useState('');
    const [student, setStudent] = useState('');
    const [description, setDescription] = useState('');
    const [date, setDate] = useState('');
    const [file, setFile] = useState('');
    const [parentsCalled, setParentsCalled] = useState('');
    const [length, setLength] = useState('');
    const [topics, setTopics] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();

        const visit = {
            advisor,
            student,
            description,
            date,
            file: file ? new URL(file) : null, // Ensure the file is converted to a URL object if provided
            parentsCalled,
            length,
            topics: topics.split(',').map(topic => topic.trim()) // Assuming topics are input as a comma-separated string
        };

        try {
            const response = await fetch('/api/Visits', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(visit)
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('Success:', data);
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <div>
            <h1>Add Visit</h1>
            <form onSubmit={handleSubmit}>
                <label>
                    Advisor:
                    <input
                        type="text"
                        value={advisor}
                        onChange={(e) => setAdvisor(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Student:
                    <input
                        type="text"
                        value={student}
                        onChange={(e) => setStudent(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Description:
                    <input
                        type="text"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Date:
                    <input
                        type="date"
                        value={date}
                        onChange={(e) => setDate(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    File URL:
                    <input
                        type="text"
                        value={file}
                        onChange={(e) => setFile(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Parents Called:
                    <input
                        type="text"
                        value={parentsCalled}
                        onChange={(e) => setParentsCalled(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Length:
                    <input
                        type="text"
                        value={length}
                        onChange={(e) => setLength(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Topics (comma separated):
                    <input
                        type="text"
                        value={topics}
                        onChange={(e) => setTopics(e.target.value)}
                    />
                </label>
                <br />
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default VisitsEditComponent;



// React DOM rendering
const visitsForm = ReactDOM.createRoot(document.getElementById('VisitsForm'));
visitsForm.render(
    <React.StrictMode>
        <VisitsEditComponent />
    </React.StrictMode>
);