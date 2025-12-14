-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 14 Des 2025 pada 11.49
-- Versi server: 10.4.32-MariaDB
-- Versi PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `badminton_db`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `booking`
--

CREATE TABLE `booking` (
  `Booking_ID` char(10) NOT NULL,
  `User_ID` char(10) NOT NULL,
  `Court_ID` char(10) NOT NULL,
  `Tanggal_Main` date DEFAULT NULL,
  `Jam_Mulai` time DEFAULT NULL,
  `Jam_Selesai` time DEFAULT NULL,
  `Total_Harga` decimal(10,2) DEFAULT NULL,
  `Status_Booking` char(20) DEFAULT NULL,
  `Waktu_Dibuat` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `booking`
--

INSERT INTO `booking` (`Booking_ID`, `User_ID`, `Court_ID`, `Tanggal_Main`, `Jam_Mulai`, `Jam_Selesai`, `Total_Harga`, `Status_Booking`, `Waktu_Dibuat`) VALUES
('B121412492', 'U002', 'C001', '2025-12-14', '09:00:00', '10:00:00', 100000.00, 'Confirmed', '2025-12-14 12:49:27'),
('B121413005', 'U002', 'C003', '2025-12-14', '10:00:00', '11:00:00', 80000.00, 'Cancelled', '2025-12-14 13:00:52'),
('B121413502', 'U002', 'C003', '2025-12-14', '15:00:00', '16:00:00', 80000.00, 'Confirmed', '2025-12-14 13:50:22'),
('B121413510', 'U002', 'C006', '2025-12-24', '17:00:00', '18:00:00', 50000.00, 'Confirmed', '2025-12-14 13:51:02'),
('B121414002', 'U9454', 'C002', '2025-12-23', '15:00:00', '16:00:00', 100000.00, 'Confirmed', '2025-12-14 14:00:23'),
('B121415034', 'U002', 'C001', '2025-12-01', '08:00:00', '09:00:00', 100000.00, 'Confirmed', '2025-12-14 15:03:40');

-- --------------------------------------------------------

--
-- Struktur dari tabel `court`
--

CREATE TABLE `court` (
  `Court_ID` char(10) NOT NULL,
  `Nama_Court` varchar(50) DEFAULT NULL,
  `Jenis_Lantai` varchar(50) DEFAULT NULL,
  `Fasilitas` varchar(100) DEFAULT NULL,
  `Harga_Per_Jam` decimal(10,2) DEFAULT NULL,
  `Gambar_URL` varchar(255) DEFAULT NULL,
  `Status_Aktif` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `court`
--

INSERT INTO `court` (`Court_ID`, `Nama_Court`, `Jenis_Lantai`, `Fasilitas`, `Harga_Per_Jam`, `Gambar_URL`, `Status_Aktif`) VALUES
('C001', 'Court A', '', 'AC, Tribun, Digital Scoreboard', 100000.00, 'https://sapabima.id/wp-content/uploads/2023/09/38.jpg', 1),
('C002', 'Court B', 'Vinyl (Pro)', 'AC, Tribun, Digital Scoreboard', 100000.00, 'https://sapabima.id/wp-content/uploads/2023/09/38.jpg', 1),
('C003', 'Court C', 'Kayu (Standard)', 'Kipas Angin, Manual Scoreboard', 80000.00, 'https://www.lantaikayu.biz/wp-content/uploads/2021/04/lantai-lapangan-badminton.jpg', 1),
('C004', 'Court D', 'Kayu (Standard)', 'Kipas Angin, Manual Scoreboard', 80000.00, 'https://www.lantaikayu.biz/wp-content/uploads/2021/04/lantai-lapangan-badminton.jpg', 1),
('C005', 'Court E', 'Interlock (Basic)', 'Kipas Angin, Manual Scoreboard', 50000.00, 'https://r-vsport.com/images/produk/lantai-badminton-finil-dan-interlock-92.jpeg', 1),
('C006', 'Court F', 'Interlock (Basic)', 'Kipas Angin, Manual Scoreboard', 50000.00, 'https://r-vsport.com/images/produk/lantai-badminton-finil-dan-interlock-92.jpeg', 1);

-- --------------------------------------------------------

--
-- Struktur dari tabel `users`
--

CREATE TABLE `users` (
  `User_ID` char(10) NOT NULL,
  `Nama_Lengkap` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Nomor_Hp` char(20) DEFAULT NULL,
  `Role` char(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `users`
--

INSERT INTO `users` (`User_ID`, `Nama_Lengkap`, `Email`, `Password`, `Nomor_Hp`, `Role`) VALUES
('U001', 'Mimin Ganteng', 'admin@smash.com', 'admin123', '081234567890', 'Admin'),
('U002', 'Zikri Member', 'zikri@gmail.com', '12345', '089876543210', 'User'),
('U9454', 'sari', 'sari@yaho.com', 'sari123', '0899999999', 'User');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `booking`
--
ALTER TABLE `booking`
  ADD PRIMARY KEY (`Booking_ID`),
  ADD KEY `User_ID` (`User_ID`),
  ADD KEY `Court_ID` (`Court_ID`);

--
-- Indeks untuk tabel `court`
--
ALTER TABLE `court`
  ADD PRIMARY KEY (`Court_ID`);

--
-- Indeks untuk tabel `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`User_ID`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `booking`
--
ALTER TABLE `booking`
  ADD CONSTRAINT `booking_ibfk_1` FOREIGN KEY (`User_ID`) REFERENCES `users` (`User_ID`),
  ADD CONSTRAINT `booking_ibfk_2` FOREIGN KEY (`Court_ID`) REFERENCES `court` (`Court_ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
