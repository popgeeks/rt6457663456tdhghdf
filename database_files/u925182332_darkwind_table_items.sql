
-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `ID` int(10) UNSIGNED NOT NULL,
  `ItemName` varchar(45) NOT NULL,
  `ItemDescription` varchar(300) NOT NULL,
  `ItemPurchase` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `ItemSellValue` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Breakable` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `BreakRate` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Charges` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `IsLore` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`ID`, `ItemName`, `ItemDescription`, `ItemPurchase`, `ItemSellValue`, `Breakable`, `BreakRate`, `Charges`, `IsLore`) VALUES
(1, 'Magic Dust', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 1 cards.', 0, 5, 0, 0, 0, 0),
(2, 'Spectral Dust', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 2 cards.', 0, 10, 0, 0, 0, 0),
(3, 'Glowing Dust', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 3 cards.', 0, 15, 0, 0, 0, 0),
(4, 'Sparkling Dust', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 4 cards.', 0, 20, 0, 0, 0, 0),
(5, 'Illuminating Dust', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 5 cards.', 0, 25, 0, 0, 0, 0),
(6, 'Glimmering Dust', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 6 cards.', 0, 30, 0, 0, 0, 0),
(7, 'Dust of Shadows', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 7 cards.', 0, 40, 0, 0, 0, 0),
(8, 'Dust of Souls', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 8 cards.', 0, 75, 0, 0, 0, 0),
(9, 'Dust of Enlightenment', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 9 cards.', 0, 100, 0, 0, 0, 0),
(10, 'Dust of Ethereal Power', 'This dust is created using the power of the moogle\'s ancient divine rod and incinerating level 10 cards.', 0, 150, 0, 0, 0, 0),
(11, 'Divine Rod of Ancient Power', 'This divine rod is a gift from the moogles to allow you to turn cards into various magical dust.  This rod has a small chance to break after repeated use.', 1000, 250, 1, 3, 0, 0),
(12, 'Divine Rod of Ethereal Power', 'This divine rod is a gift from the moogles to allow you to turn cards into various magical dust.  This rod has a less chance to break than the Divine Rod of Ancient Power.', 2500, 500, 1, 1, 0, 0),
(13, 'Enchantment Rod', 'This rod can fuse low-grade items onto parchment.', 500, 200, 1, 1, 0, 0),
(14, 'Enchanted Parchment', 'Parchment paper enchanted by magic dust from burned triple triad cards.', 100, 25, 0, 0, 0, 0);
