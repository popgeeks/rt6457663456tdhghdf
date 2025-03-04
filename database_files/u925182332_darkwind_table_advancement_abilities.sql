
-- --------------------------------------------------------

--
-- Table structure for table `advancement_abilities`
--

CREATE TABLE `advancement_abilities` (
  `id` int(10) UNSIGNED NOT NULL,
  `keyword` varchar(45) DEFAULT '',
  `name` varchar(60) DEFAULT '',
  `description` varchar(2000) DEFAULT '',
  `minlvl` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `levels` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `points` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `lvl1_mod` varchar(10) NOT NULL DEFAULT '0',
  `lvl2_mod` varchar(10) NOT NULL DEFAULT '0',
  `lvl3_mod` varchar(10) NOT NULL DEFAULT '0',
  `lvl4_mod` varchar(10) NOT NULL DEFAULT '0',
  `lvl5_mod` varchar(10) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `advancement_abilities`
--

INSERT INTO `advancement_abilities` (`id`, `keyword`, `name`, `description`, `minlvl`, `levels`, `points`, `lvl1_mod`, `lvl2_mod`, `lvl3_mod`, `lvl4_mod`, `lvl5_mod`) VALUES
(1, 'golden', 'Golden Touch', 'Gain King Midas\'s golden touch.  You may train this ability and gain 5% more gold per level.  Level 18 or greater is required.', 18, 5, 2, '.05', '.10', '.15', '.20', '.25'),
(2, 'darkys', 'Darkys Apprentice', 'Become Darky\'s apprentice and learn skills faster than before.  You may train this ability and gain 2% more experience per level with a maximum of 5 levels.  Level 18 or greater is required.', 18, 5, 2, '.02', '.04', '.06', '.08', '.10'),
(3, 'high', 'High Roller', 'Lady Luck will smile on you by increasing your dice odds in gaining better odds in acquiring higher level cards in the card packs.  You may train this ability and gain 1% better odds per level.  Level 18 or greater is required.', 18, 5, 2, '.01', '.02', '.03', '.04', '.05'),
(4, 'busy', 'Busy Bee', 'By training this ability, you gain 150% active points.  This is trainable one time. Level 18 or greater is required.', 18, 1, 3, '0', '0', '0', '0', '0'),
(5, 'bountiful', 'Bountiful Bazaar', 'By training this ability, you gain the ability to increase the maximum card cap in the player merchants.  This is trainable three times and requires a level of 20 or greater.', 20, 3, 2, '1', '2', '3', '4', '5'),
(6, 'petty', 'Petty Pockets', 'By training this ability, you cut the transaction fee per card purchase in the player merchant shops by one-third.  This is trainable only once and requires a level of 20 or greater.', 20, 1, 3, '0.67', '0', '0', '0', '0'),
(7, 'full', 'Full Scope', 'By training this ability, you gain the ability to increase your search maximum in the player merchant search screen by 10.  This is trainable twice and requires a level of 15 or greater.', 15, 2, 2, '0', '0', '0', '0', '0'),
(9, 'alter', 'Alter Decision', 'By training this ability, you gain the ability to alter the gender of your avatar in the avatar shop. This can be trained an unlimited amount of times and will not reflect that you have trained this ability.', 10, 1, 1, '0', '0', '0', '0', '0'),
(10, 'true', 'True Rewards', 'By training this ability, you increase the cap of gold earned as you change ranks by 15%, 25%, and 50% respectively.', 15, 3, 2, '0.15', '0.25', '0.50', '0', '0');
