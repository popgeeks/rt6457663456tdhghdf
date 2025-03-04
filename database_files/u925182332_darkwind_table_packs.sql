
-- --------------------------------------------------------

--
-- Table structure for table `packs`
--

CREATE TABLE `packs` (
  `pack_id` int(10) UNSIGNED NOT NULL,
  `pack_key` varchar(75) NOT NULL,
  `pack_description` varchar(500) NOT NULL,
  `pack_tooltip` varchar(500) DEFAULT NULL,
  `pack_price` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `pack_crystals` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `packs`
--

INSERT INTO `packs` (`pack_id`, `pack_key`, `pack_description`, `pack_tooltip`, `pack_price`, `pack_crystals`) VALUES
(1, 'Starter Pack - %%DECK%%', 'The starter pack is the most basic pack in the game. For a small fee, you can buy a pack of 5 cards, with a very low chance of receiving an uncommon or higher card.', '<p><strong>Starter Pack</strong></p><p><ul><li>5 Random Cards from %%DECK%%</li><li>Low Chance at a Uncommon+ Card.</li></ul></p>', 500, 0),
(2, 'Standard Pack - %%DECK%%', 'The standard pack is a larger pack and useful for those looking for more powerful cards. This pack contains 6 cards, with a guaranteed rare or higher card and a chance to receive a second.', '<p><strong>Standard Pack</strong></p><p><ul><li>6 Random Cards from %%DECK%%</li><li>Guaranteed 1 Rare+ Card, with the chance of a second.</li></ul></p>', 1000, 200),
(3, 'Jumbo Pack - %%DECK%%', 'The jumbo pack is a much larger pack for the active players or those with deep gold pockets. This pack contains 12 cards, with 2 guaranteed rare or higher cards and a chance to receive a third.', '<p><strong>Jumbo Pack</strong></p><p><ul><li>12 Random Cards from %%DECK%%</li><li>Guaranteed 2 Rare+ Cards, with the chance of a third.</li></ul></p>', 2250, 400),
(4, 'Premium Jumbo Pack - %%DECK%%', 'The premium jumbo pack is an elite level pack with 20 cards, with 4 guaranteed rare or higher cards and a chance for a 5th card.', '<p><strong>Premium Jumbo Pack</strong></p><p><ul><li>20 Random Cards from %%DECK%%</li><li>Guaranteed 4 Rare+ Cards, with the chance of a fifth.</li></ul></p>', 4000, 700);
