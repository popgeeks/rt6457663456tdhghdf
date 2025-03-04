
-- --------------------------------------------------------

--
-- Table structure for table `pendingreferals`
--

CREATE TABLE `pendingreferals` (
  `ID` int(10) UNSIGNED NOT NULL,
  `Referal` varchar(45) NOT NULL,
  `ReferingPlayer` varchar(45) NOT NULL,
  `Stamp` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `pendingreferals`
--

INSERT INTO `pendingreferals` (`ID`, `Referal`, `ReferingPlayer`, `Stamp`) VALUES
(2, 'ktom', 'gerbenben', '2009-04-16 20:39:48'),
(6, 'ladydeath', 'affa', '2009-05-17 08:01:25'),
(7, 'parasite', 'squall-rank9', '2009-06-11 23:26:48'),
(8, 'xico13', 'xicotencatl', '2009-06-21 04:00:32'),
(10, 'Ayuna', 'J102Y', '2009-07-25 03:26:36'),
(11, 'geri13', 'gerbenben', '2009-07-25 08:32:42'),
(12, 'AsheDalmasca', 'kochanie1974', '2009-08-10 23:05:20'),
(16, 'Frostemma', 'namine', '2009-10-04 23:19:34'),
(19, 'battousai98', 'robo', '2009-10-14 15:38:18'),
(20, 'ZackFairrr', 'legendary', '2009-11-02 17:05:48'),
(21, 'Leviathanx42', 'male', '2009-11-12 16:42:21'),
(22, 'Team', 'Jinvicious', '2009-12-08 22:21:20'),
(23, 'nsain', 'google.com', '2010-01-21 22:11:16'),
(24, 'MoogleKate', 'ZackFairrr', '2010-05-16 08:09:48'),
(25, 'fate', 'fuckoff', '2010-05-30 04:22:47'),
(26, 'corin3690', 'google', '2010-07-11 16:13:52'),
(27, 'elvoret659', 'fuckoff', '2010-09-23 05:27:04'),
(28, 'alukardian', 'LedZep', '2010-10-02 00:00:54'),
(29, 'albanchurro', 'Kenny', '2010-12-02 01:40:53'),
(30, 'Yukarie', 'Renoshka', '2011-01-23 14:52:30'),
(31, 'david991', 'sfax', '2011-02-14 18:39:17'),
(32, 'Dantushk', 'Yukarie', '2011-03-28 20:09:09'),
(33, 'crimiboss', 'sukkel', '2011-07-03 21:05:24'),
(34, 'Mariyasha', 'legendary', '2011-12-10 20:48:22');
