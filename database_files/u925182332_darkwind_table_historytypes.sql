
-- --------------------------------------------------------

--
-- Table structure for table `historytypes`
--

CREATE TABLE `historytypes` (
  `typePK` int(10) UNSIGNED NOT NULL,
  `description` varchar(255) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `historytypes`
--

INSERT INTO `historytypes` (`typePK`, `description`) VALUES
(1, 'Game Abandon'),
(2, 'System'),
(3, 'Rank Penalty'),
(4, 'Player Shop Sale'),
(5, 'Ticket Purchase'),
(6, 'Surrender'),
(7, 'Card Packs'),
(8, 'New Decks'),
(9, 'Sell Cards'),
(10, 'Player Shop Change'),
(11, 'Logins'),
(12, 'EXP Triggers'),
(13, 'Trades'),
(14, 'Tournament Buy-Ins'),
(15, ''),
(16, ''),
(17, 'Gold Transactions'),
(18, 'AA Point Increase'),
(19, 'AA Point Train'),
(20, 'Card Penalty'),
(21, 'Bank Card Remove'),
(22, 'Bank Deposit'),
(23, 'Bank Withdrawl'),
(24, 'Auction GP Add'),
(26, 'Auction GP Delete'),
(27, 'Auction Win'),
(28, 'Bank Card Add'),
(29, 'Password Recovery');
