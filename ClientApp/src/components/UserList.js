import { use, useEffect, useState } from 'react';
import axios from 'axios';

export default function UserList({ onUserSelect, users }) {


    return (
        <ul style={{ listStyle: 'none', padding: 0 }}>
            {users && users.map(user => (
                <li key={user.id_user} onClick={() => onUserSelect(user)} style={{ padding: '8px', cursor: 'pointer', borderBottom: '1px solid #ccc' }}>
                    {user.name}
                </li>
            ))}
        </ul>
    );
}
