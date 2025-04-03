# SourDuckWannaBet

**SourDuckWannaBet** is an interactive, feature-rich betting platform built to provide users with a seamless experience for creating, managing, and resolving bets. Whether you're placing bets on sports, games, or personal challenges, the app ensures everything is transparent, fun, and fair. Built with **ASP.NET** and **C#**, SourDuckWannaBet integrates a **Supabase** backend database and is deployed via **Azure** for easy access and scalability.

![Screenshot_of_login](https://github.com/NJMarzina/SourDuckWannaBet/blob/master/WBLogin_sc1.png)
![Screenshot_of_dashboard](https://github.com/NJMarzina/SourDuckWannaBet/blob/master/WBDashboard_sc1.png)
![Screenshot_of_dashboard2](https://github.com/NJMarzina/SourDuckWannaBet/blob/master/WBDashboard_sc2.png)

---

### 🔗 [Demo here](https://wannabet-apczh6bmfbfvfef8.centralus-01.azurewebsites.net/WBLogin.aspx)

---

## Key Features

- **User Creation**: Easily create new users and manage profiles.
- **Bet Management**: Send, modify, and complete bets with real-time updates.
- **Balance Alteration**: Adjust user balances based on bet outcomes, ensuring accurate financial tracking.
- **Bet Modification**: Users can modify their bets before they’re completed, providing flexibility.
- **Bet Completion**: Once a bet is completed, users are notified and their balances updated accordingly.
  
This platform is built for a smooth betting experience, allowing users to make informed decisions and manage their wagers with confidence.

---

## Backend Technology

**SourDuckWannaBet** uses **ASP.NET** and **C#** for its backend, ensuring high performance, security, and scalability. The backend is seamlessly connected to a **Supabase** database, providing real-time capabilities and secure data management.

### Supabase Database Integration

SourDuckWannaBet leverages **Supabase** for backend database services, using a **PostgreSQL** database to manage users, bets, and transactions.

1. **User Management**:
   - User profiles are stored securely in the `users` table with data like usernames, balances, and betting history.
   - Authentication and security are managed by Supabase for ease and safety.
  
2. **Betting System**:
   - All bets are tracked in the `bets` table, with details such as participants, amounts, bet statuses, and timestamps.
   - Real-time updates from Supabase ensure instant feedback on bet actions and balance changes.

3. **Transaction History**:
   - Financial transactions, including bet placements, modifications, and payouts, are stored in the `transactions` table for full transparency.

4. **Scalability & Performance**:
   - Supabase's PostgreSQL backend provides high scalability to handle large volumes of users and bets efficiently.

5. **API Access**:
   - Supabase's RESTful API allows for easy querying, updating, and managing of data between the frontend and the database.

---

## Future Updates

**What's Coming Next**:

- **Edit Profile**: We’re adding a feature that allows users to edit their profiles, including changing usernames and updating other personal details.
  
- **Game Room**: We’re also working on integrating a gaming area with classic games like **Dice**, **Roulette**, **Blackjack**, and more, bringing a new level of fun to the platform!

---

## Database Management with Supabase

You can explore and manage the database directly via the **Supabase Dashboard**:  
👉 [Supabase Dashboard](https://supabase.com/dashboard/project/sliykwxeogrnrqgysvrh)

---

## Contact

For questions, feedback, or support, feel free to reach out:

- **Email**: OhMarzy23@gmail.com
- **GitHub Issues**: [Open an Issue](https://github.com/NJMarzina/SourDuckWannaBet/issues)

---

**SourDuckWannaBet** – Place your bets, have some fun! 🦆🎲
