
-- --------------------------------------------------------

--
-- Table structure for table `veteran_advancement`
--

CREATE TABLE `veteran_advancement` (
  `id` int(10) UNSIGNED NOT NULL,
  `keyword` varchar(45) DEFAULT '',
  `name` varchar(60) DEFAULT '',
  `description` varchar(2000) DEFAULT '',
  `minlvl` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `levels` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `points` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `mod` varchar(10) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `veteran_advancement`
--

INSERT INTO `veteran_advancement` (`id`, `keyword`, `name`, `description`, `minlvl`, `levels`, `points`, `mod`) VALUES
(1, 'greedy', 'Greedy Fingers', 'Increase gold gained in memberships by 2% per level', 1, 10, 1, '0.02'),
(2, 'active', 'Active Boost', 'Increase active points gained in memberships by 2% per level', 1, 10, 1, '0.02'),
(3, 'bounty', 'Bounty of the Virtuous', 'Gain an extra token each time a membership higher than gold is purchased.', 1, 1, 3, '0.00'),
(4, 'gold', 'Gold Enhance', 'Increase the gold benefit earned each game by 5% per level', 1, 8, 2, '0.05'),
(5, 'experience', 'Experience Enhance', 'Increase the experience benefit earned each game by 5% per level', 1, 8, 2, '0.05'),
(6, 'token', 'Token Exchange', 'Exchange 1 VA point for 1 Token', 1, 1, 1, '0.00'),
(7, 'extended', 'Extended Rewards', 'Increase the amount of gold earned per level by 5%', 1, 20, 1, '0.05'),
(8, 'enhance', 'Enhance Merchant', 'Increase the amount of cards you can add to your shop by 5 per level', 1, 20, 1, '5.00'),
(9, 'vip', 'VIP Roller', 'Increase the luck of earning a high level card in a pack by 1% per level', 1, 3, 2, '0.01');
