
-- --------------------------------------------------------

--
-- Table structure for table `administrators`
--

CREATE TABLE `administrators` (
  `player` varchar(15) NOT NULL DEFAULT '',
  `status` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `passkey` varchar(8) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `administrators`
--

INSERT INTO `administrators` (`player`, `status`, `passkey`) VALUES
('atomicstorm', 4, '41257448'),
('forumadmin', 5, '41257448'),
('jackie', 3, '30948101'),
('Yikwang', 2, '23590821'),
('zeromus', 2, '14327517');
