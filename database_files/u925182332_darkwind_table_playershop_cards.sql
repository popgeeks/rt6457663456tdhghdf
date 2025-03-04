
-- --------------------------------------------------------

--
-- Table structure for table `playershop_cards`
--

CREATE TABLE `playershop_cards` (
  `player` varchar(20) NOT NULL,
  `cardname` varchar(50) NOT NULL,
  `price` varchar(10) NOT NULL,
  `id` bigint(20) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;
