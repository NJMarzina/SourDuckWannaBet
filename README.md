
---

# SourDuckWannaBet

**SourDuckWannaBet** is a dynamic and interactive betting platform designed to bring users together for friendly wagers. Whether you're betting on sports, games, or personal challenges, SourDuckWannaBet provides a seamless experience for creating, managing, and resolving bets. With a focus on user-friendly design and robust functionality, this app ensures that every bet is fair, transparent, and fun.

---

## Key Features

- **Create Bets**: Users can create bets by specifying the terms, amounts, and participants. The app supports both one-sided and two-sided bets.
- **Accept or Deny Bets**: Recipients can accept or deny incoming bets. If accepted, the bet amounts are locked in. If denied, the sender's amount is refunded.
- **Balance Management**: The app automatically deducts and refunds amounts from user balances based on bet outcomes.
- **Transaction History**: Every bet and balance change is recorded in the `transactions` table, providing a clear audit trail.
- **Real-Time Updates**: Users receive real-time updates on bet statuses and balance changes.
- **Mediator Support**: Bets can involve a mediator to ensure fairness and resolve disputes.

---

## Database Support Provided by Supabase

SourDuckWannaBet leverages **Supabase**, an open-source Firebase alternative, for its backend database needs. Supabase provides a powerful and scalable PostgreSQL database, enabling seamless data management and real-time functionality.

### How We Use Supabase

1. **User Management**:
   - User profiles, including usernames, balances, and betting history, are stored in the `users` table.
   - Supabase handles authentication and user data securely.

2. **Bet Management**:
   - Bets are stored in the `bets` table, which tracks details such as sender, receiver, amounts, status, and timestamps.
   - Supabase's real-time capabilities ensure that users receive instant updates on bet statuses.

3. **Transaction Tracking**:
   - All financial transactions, including bet creation, acceptance, and refunds, are recorded in the `transactions` table.
   - This ensures transparency and provides a complete history of user activity.

4. **Scalability and Performance**:
   - Supabase's PostgreSQL database is highly scalable, ensuring that the app can handle a growing number of users and bets without compromising performance.

5. **RESTful API**:
   - Supabase provides a RESTful API that allows the app to interact with the database seamlessly. This includes querying, inserting, updating, and deleting records.

6. **Security**:
   - Supabase ensures data security with built-in authentication, row-level security, and encryption.

---

## Supabase Dashboard

You can explore the database structure and manage data directly through the **Supabase Dashboard**:
👉 [Supabase Dashboard](https://supabase.com/dashboard/project/sliykwxeogrnrqgysvrh)

---

## Getting Started

### Prerequisites
- .NET Framework (for the backend)
- Supabase account and project setup
- Basic understanding of REST APIs and PostgreSQL

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/SourDuckWannaBet.git
   ```
2. Set up your Supabase project and update the connection details in the app configuration.
3. Run the app locally or deploy it to your preferred hosting platform.

---


## Acknowledgments

- **Supabase** for providing a robust and scalable database solution.
- The .NET community for their extensive resources and support.
- All contributors and users who help make SourDuckWannaBet better every day.

---

## Contact

For questions, feedback, or support, please reach out to us at:
- **Email**: NJMarzina@gmail.com
- **GitHub Issues**: [Open an Issue](https://github.com/NJMarzina/SourDuckWannaBet/issues)

---

**SourDuckWannaBet** – Where every bet is a win-win! 🦆🎲

---
