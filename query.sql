USE [LearnEase]

/*========================================================
  1. LANGUAGE & DIALECT
========================================================*/
INSERT INTO Languages(LanguageId, Name)
VALUES ('1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', N'English');

INSERT INTO Dialects (DialectId, LanguageId, Name, Region, AccentCode, Description, VoiceSampleUrl, IsAvailable)
VALUES
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'General American', N'United States', 'en-US',
   N'Standard American English accent',
   'https://cdn.learnEase.com/audio/accent/en_us_sample.mp3', 1),
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F2', '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'Received Pronunciation', N'United Kingdom', 'en-GB',
   N'Standard British English accent',
   'https://cdn.learnEase.com/audio/accent/en_gb_sample.mp3', 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Australia)', 'Australia', 'en-AU', 'Australian English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (India)', 'India', 'en-IN', 'Indian English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Canada)', 'Canada', 'en-CA', 'Canadian English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (New Zealand)', 'New Zealand', 'en-NZ', 'New Zealand English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Singapore)', 'Singapore', 'en-SG', 'Singapore English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (South Africa)', 'South Africa', 'en-ZA', 'South African English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Philippines)', 'Philippines', 'en-PH', 'Philippine English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Hong Kong)', 'Hong Kong', 'en-HK', 'Hong Kong English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Ireland)', 'Ireland', 'en-IE', 'Irish English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Kenya)', 'Kenya', 'en-KE', 'Kenyan English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Tanzania)', 'Tanzania', 'en-TZ', 'Tanzanian English accent', NULL, 1);

/*========================================================
  2. USER & SETTING
========================================================*/
INSERT INTO [Users]
  (UserId, Username, Email, Password, AvatarUrl, CreatedAt)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781', 'vuong', 'vuong@example.com', '123', NULL, SYSUTCDATETIME());

INSERT INTO UserSettings
  (UserId, PreferredDialectId, PlaybackSpeed, NotificationOn)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1.0, 1);

/*========================================================
  3. VOCABULARY & SPEAKING EXERCISE
========================================================*/
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010001-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word1', 'https://cdn.learnEase.com/audio/vocab/word1.mp3', NULL, N'["word1a","word1b","word1c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010002-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word2', 'https://cdn.learnEase.com/audio/vocab/word2.mp3', NULL, N'["word2a","word2b","word2c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010003-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word3', 'https://cdn.learnEase.com/audio/vocab/word3.mp3', NULL, N'["word3a","word3b","word3c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010004-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word4', 'https://cdn.learnEase.com/audio/vocab/word4.mp3', NULL, N'["word4a","word4b","word4c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010005-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word5', 'https://cdn.learnEase.com/audio/vocab/word5.mp3', NULL, N'["word5a","word5b","word5c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010006-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word6', 'https://cdn.learnEase.com/audio/vocab/word6.mp3', NULL, N'["word6a","word6b","word6c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010007-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word7', 'https://cdn.learnEase.com/audio/vocab/word7.mp3', NULL, N'["word7a","word7b","word7c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010008-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word8', 'https://cdn.learnEase.com/audio/vocab/word8.mp3', NULL, N'["word8a","word8b","word8c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010009-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word9', 'https://cdn.learnEase.com/audio/vocab/word9.mp3', NULL, N'["word9a","word9b","word9c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010010-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word10', 'https://cdn.learnEase.com/audio/vocab/word10.mp3', NULL, N'["word10a","word10b","word10c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010011-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word11', 'https://cdn.learnEase.com/audio/vocab/word11.mp3', NULL, N'["word11a","word11b","word11c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010012-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word12', 'https://cdn.learnEase.com/audio/vocab/word12.mp3', NULL, N'["word12a","word12b","word12c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010013-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word13', 'https://cdn.learnEase.com/audio/vocab/word13.mp3', NULL, N'["word13a","word13b","word13c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010014-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word14', 'https://cdn.learnEase.com/audio/vocab/word14.mp3', NULL, N'["word14a","word14b","word14c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010015-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word15', 'https://cdn.learnEase.com/audio/vocab/word15.mp3', NULL, N'["word15a","word15b","word15c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010016-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word16', 'https://cdn.learnEase.com/audio/vocab/word16.mp3', NULL, N'["word16a","word16b","word16c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010017-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word17', 'https://cdn.learnEase.com/audio/vocab/word17.mp3', NULL, N'["word17a","word17b","word17c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010018-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word18', 'https://cdn.learnEase.com/audio/vocab/word18.mp3', NULL, N'["word18a","word18b","word18c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010019-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word19', 'https://cdn.learnEase.com/audio/vocab/word19.mp3', NULL, N'["word19a","word19b","word19c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010020-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word20', 'https://cdn.learnEase.com/audio/vocab/word20.mp3', NULL, N'["word20a","word20b","word20c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010021-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word21', 'https://cdn.learnEase.com/audio/vocab/word21.mp3', NULL, N'["word21a","word21b","word21c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010022-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word22', 'https://cdn.learnEase.com/audio/vocab/word22.mp3', NULL, N'["word22a","word22b","word22c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010023-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word23', 'https://cdn.learnEase.com/audio/vocab/word23.mp3', NULL, N'["word23a","word23b","word23c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010024-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word24', 'https://cdn.learnEase.com/audio/vocab/word24.mp3', NULL, N'["word24a","word24b","word24c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010025-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word25', 'https://cdn.learnEase.com/audio/vocab/word25.mp3', NULL, N'["word25a","word25b","word25c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010026-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word26', 'https://cdn.learnEase.com/audio/vocab/word26.mp3', NULL, N'["word26a","word26b","word26c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010027-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word27', 'https://cdn.learnEase.com/audio/vocab/word27.mp3', NULL, N'["word27a","word27b","word27c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010028-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word28', 'https://cdn.learnEase.com/audio/vocab/word28.mp3', NULL, N'["word28a","word28b","word28c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010029-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word29', 'https://cdn.learnEase.com/audio/vocab/word29.mp3', NULL, N'["word29a","word29b","word29c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010030-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word30', 'https://cdn.learnEase.com/audio/vocab/word30.mp3', NULL, N'["word30a","word30b","word30c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010031-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word31', 'https://cdn.learnEase.com/audio/vocab/word31.mp3', NULL, N'["word31a","word31b","word31c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010032-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word32', 'https://cdn.learnEase.com/audio/vocab/word32.mp3', NULL, N'["word32a","word32b","word32c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010033-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word33', 'https://cdn.learnEase.com/audio/vocab/word33.mp3', NULL, N'["word33a","word33b","word33c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010034-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word34', 'https://cdn.learnEase.com/audio/vocab/word34.mp3', NULL, N'["word34a","word34b","word34c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010035-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word35', 'https://cdn.learnEase.com/audio/vocab/word35.mp3', NULL, N'["word35a","word35b","word35c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010036-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word36', 'https://cdn.learnEase.com/audio/vocab/word36.mp3', NULL, N'["word36a","word36b","word36c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010037-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word37', 'https://cdn.learnEase.com/audio/vocab/word37.mp3', NULL, N'["word37a","word37b","word37c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010038-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word38', 'https://cdn.learnEase.com/audio/vocab/word38.mp3', NULL, N'["word38a","word38b","word38c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010039-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word39', 'https://cdn.learnEase.com/audio/vocab/word39.mp3', NULL, N'["word39a","word39b","word39c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010040-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word40', 'https://cdn.learnEase.com/audio/vocab/word40.mp3', NULL, N'["word40a","word40b","word40c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010041-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word41', 'https://cdn.learnEase.com/audio/vocab/word41.mp3', NULL, N'["word41a","word41b","word41c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010042-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word42', 'https://cdn.learnEase.com/audio/vocab/word42.mp3', NULL, N'["word42a","word42b","word42c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010043-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word43', 'https://cdn.learnEase.com/audio/vocab/word43.mp3', NULL, N'["word43a","word43b","word43c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010044-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word44', 'https://cdn.learnEase.com/audio/vocab/word44.mp3', NULL, N'["word44a","word44b","word44c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010045-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word45', 'https://cdn.learnEase.com/audio/vocab/word45.mp3', NULL, N'["word45a","word45b","word45c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010046-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word46', 'https://cdn.learnEase.com/audio/vocab/word46.mp3', NULL, N'["word46a","word46b","word46c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010047-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word47', 'https://cdn.learnEase.com/audio/vocab/word47.mp3', NULL, N'["word47a","word47b","word47c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010048-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word48', 'https://cdn.learnEase.com/audio/vocab/word48.mp3', NULL, N'["word48a","word48b","word48c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010049-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word49', 'https://cdn.learnEase.com/audio/vocab/word49.mp3', NULL, N'["word49a","word49b","word49c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010050-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word50', 'https://cdn.learnEase.com/audio/vocab/word50.mp3', NULL, N'["word50a","word50b","word50c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010051-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word51', 'https://cdn.learnEase.com/audio/vocab/word51.mp3', NULL, N'["word51a","word51b","word51c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010052-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word52', 'https://cdn.learnEase.com/audio/vocab/word52.mp3', NULL, N'["word52a","word52b","word52c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010053-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word53', 'https://cdn.learnEase.com/audio/vocab/word53.mp3', NULL, N'["word53a","word53b","word53c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010054-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word54', 'https://cdn.learnEase.com/audio/vocab/word54.mp3', NULL, N'["word54a","word54b","word54c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010055-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word55', 'https://cdn.learnEase.com/audio/vocab/word55.mp3', NULL, N'["word55a","word55b","word55c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010056-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word56', 'https://cdn.learnEase.com/audio/vocab/word56.mp3', NULL, N'["word56a","word56b","word56c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010057-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word57', 'https://cdn.learnEase.com/audio/vocab/word57.mp3', NULL, N'["word57a","word57b","word57c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010058-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word58', 'https://cdn.learnEase.com/audio/vocab/word58.mp3', NULL, N'["word58a","word58b","word58c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010059-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word59', 'https://cdn.learnEase.com/audio/vocab/word59.mp3', NULL, N'["word59a","word59b","word59c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010060-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word60', 'https://cdn.learnEase.com/audio/vocab/word60.mp3', NULL, N'["word60a","word60b","word60c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010061-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word61', 'https://cdn.learnEase.com/audio/vocab/word61.mp3', NULL, N'["word61a","word61b","word61c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010062-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word62', 'https://cdn.learnEase.com/audio/vocab/word62.mp3', NULL, N'["word62a","word62b","word62c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010063-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word63', 'https://cdn.learnEase.com/audio/vocab/word63.mp3', NULL, N'["word63a","word63b","word63c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010064-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word64', 'https://cdn.learnEase.com/audio/vocab/word64.mp3', NULL, N'["word64a","word64b","word64c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010065-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word65', 'https://cdn.learnEase.com/audio/vocab/word65.mp3', NULL, N'["word65a","word65b","word65c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010066-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word66', 'https://cdn.learnEase.com/audio/vocab/word66.mp3', NULL, N'["word66a","word66b","word66c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010067-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word67', 'https://cdn.learnEase.com/audio/vocab/word67.mp3', NULL, N'["word67a","word67b","word67c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010068-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word68', 'https://cdn.learnEase.com/audio/vocab/word68.mp3', NULL, N'["word68a","word68b","word68c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010069-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word69', 'https://cdn.learnEase.com/audio/vocab/word69.mp3', NULL, N'["word69a","word69b","word69c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010070-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word70', 'https://cdn.learnEase.com/audio/vocab/word70.mp3', NULL, N'["word70a","word70b","word70c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010071-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word71', 'https://cdn.learnEase.com/audio/vocab/word71.mp3', NULL, N'["word71a","word71b","word71c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010072-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word72', 'https://cdn.learnEase.com/audio/vocab/word72.mp3', NULL, N'["word72a","word72b","word72c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010073-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word73', 'https://cdn.learnEase.com/audio/vocab/word73.mp3', NULL, N'["word73a","word73b","word73c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010074-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word74', 'https://cdn.learnEase.com/audio/vocab/word74.mp3', NULL, N'["word74a","word74b","word74c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010075-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word75', 'https://cdn.learnEase.com/audio/vocab/word75.mp3', NULL, N'["word75a","word75b","word75c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010076-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word76', 'https://cdn.learnEase.com/audio/vocab/word76.mp3', NULL, N'["word76a","word76b","word76c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010077-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word77', 'https://cdn.learnEase.com/audio/vocab/word77.mp3', NULL, N'["word77a","word77b","word77c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010078-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word78', 'https://cdn.learnEase.com/audio/vocab/word78.mp3', NULL, N'["word78a","word78b","word78c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010079-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word79', 'https://cdn.learnEase.com/audio/vocab/word79.mp3', NULL, N'["word79a","word79b","word79c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010080-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word80', 'https://cdn.learnEase.com/audio/vocab/word80.mp3', NULL, N'["word80a","word80b","word80c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010081-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word81', 'https://cdn.learnEase.com/audio/vocab/word81.mp3', NULL, N'["word81a","word81b","word81c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010082-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word82', 'https://cdn.learnEase.com/audio/vocab/word82.mp3', NULL, N'["word82a","word82b","word82c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010083-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word83', 'https://cdn.learnEase.com/audio/vocab/word83.mp3', NULL, N'["word83a","word83b","word83c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010084-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word84', 'https://cdn.learnEase.com/audio/vocab/word84.mp3', NULL, N'["word84a","word84b","word84c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010085-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word85', 'https://cdn.learnEase.com/audio/vocab/word85.mp3', NULL, N'["word85a","word85b","word85c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010086-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word86', 'https://cdn.learnEase.com/audio/vocab/word86.mp3', NULL, N'["word86a","word86b","word86c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010087-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word87', 'https://cdn.learnEase.com/audio/vocab/word87.mp3', NULL, N'["word87a","word87b","word87c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010088-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word88', 'https://cdn.learnEase.com/audio/vocab/word88.mp3', NULL, N'["word88a","word88b","word88c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010089-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word89', 'https://cdn.learnEase.com/audio/vocab/word89.mp3', NULL, N'["word89a","word89b","word89c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010090-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word90', 'https://cdn.learnEase.com/audio/vocab/word90.mp3', NULL, N'["word90a","word90b","word90c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010091-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word91', 'https://cdn.learnEase.com/audio/vocab/word91.mp3', NULL, N'["word91a","word91b","word91c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010092-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word92', 'https://cdn.learnEase.com/audio/vocab/word92.mp3', NULL, N'["word92a","word92b","word92c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010093-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word93', 'https://cdn.learnEase.com/audio/vocab/word93.mp3', NULL, N'["word93a","word93b","word93c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010094-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word94', 'https://cdn.learnEase.com/audio/vocab/word94.mp3', NULL, N'["word94a","word94b","word94c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010095-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word95', 'https://cdn.learnEase.com/audio/vocab/word95.mp3', NULL, N'["word95a","word95b","word95c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010096-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word96', 'https://cdn.learnEase.com/audio/vocab/word96.mp3', NULL, N'["word96a","word96b","word96c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010097-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word97', 'https://cdn.learnEase.com/audio/vocab/word97.mp3', NULL, N'["word97a","word97b","word97c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010098-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word98', 'https://cdn.learnEase.com/audio/vocab/word98.mp3', NULL, N'["word98a","word98b","word98c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010099-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word99', 'https://cdn.learnEase.com/audio/vocab/word99.mp3', NULL, N'["word99a","word99b","word99c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010100-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word100', 'https://cdn.learnEase.com/audio/vocab/word100.mp3', NULL, N'["word100a","word100b","word100c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010101-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word101', 'https://cdn.learnEase.com/audio/vocab/word101.mp3', NULL, N'["word101a","word101b","word101c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010102-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word102', 'https://cdn.learnEase.com/audio/vocab/word102.mp3', NULL, N'["word102a","word102b","word102c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010103-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word103', 'https://cdn.learnEase.com/audio/vocab/word103.mp3', NULL, N'["word103a","word103b","word103c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010104-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word104', 'https://cdn.learnEase.com/audio/vocab/word104.mp3', NULL, N'["word104a","word104b","word104c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010105-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word105', 'https://cdn.learnEase.com/audio/vocab/word105.mp3', NULL, N'["word105a","word105b","word105c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010106-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word106', 'https://cdn.learnEase.com/audio/vocab/word106.mp3', NULL, N'["word106a","word106b","word106c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010107-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word107', 'https://cdn.learnEase.com/audio/vocab/word107.mp3', NULL, N'["word107a","word107b","word107c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010108-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word108', 'https://cdn.learnEase.com/audio/vocab/word108.mp3', NULL, N'["word108a","word108b","word108c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010109-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word109', 'https://cdn.learnEase.com/audio/vocab/word109.mp3', NULL, N'["word109a","word109b","word109c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010110-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word110', 'https://cdn.learnEase.com/audio/vocab/word110.mp3', NULL, N'["word110a","word110b","word110c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010111-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word111', 'https://cdn.learnEase.com/audio/vocab/word111.mp3', NULL, N'["word111a","word111b","word111c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010112-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word112', 'https://cdn.learnEase.com/audio/vocab/word112.mp3', NULL, N'["word112a","word112b","word112c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010113-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word113', 'https://cdn.learnEase.com/audio/vocab/word113.mp3', NULL, N'["word113a","word113b","word113c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010114-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word114', 'https://cdn.learnEase.com/audio/vocab/word114.mp3', NULL, N'["word114a","word114b","word114c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010115-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word115', 'https://cdn.learnEase.com/audio/vocab/word115.mp3', NULL, N'["word115a","word115b","word115c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010116-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word116', 'https://cdn.learnEase.com/audio/vocab/word116.mp3', NULL, N'["word116a","word116b","word116c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010117-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word117', 'https://cdn.learnEase.com/audio/vocab/word117.mp3', NULL, N'["word117a","word117b","word117c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010118-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word118', 'https://cdn.learnEase.com/audio/vocab/word118.mp3', NULL, N'["word118a","word118b","word118c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010119-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word119', 'https://cdn.learnEase.com/audio/vocab/word119.mp3', NULL, N'["word119a","word119b","word119c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010120-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word120', 'https://cdn.learnEase.com/audio/vocab/word120.mp3', NULL, N'["word120a","word120b","word120c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010121-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word121', 'https://cdn.learnEase.com/audio/vocab/word121.mp3', NULL, N'["word121a","word121b","word121c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010122-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word122', 'https://cdn.learnEase.com/audio/vocab/word122.mp3', NULL, N'["word122a","word122b","word122c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010123-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word123', 'https://cdn.learnEase.com/audio/vocab/word123.mp3', NULL, N'["word123a","word123b","word123c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010124-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word124', 'https://cdn.learnEase.com/audio/vocab/word124.mp3', NULL, N'["word124a","word124b","word124c"]');
INSERT INTO VocabularyItems (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson) VALUES ('C0010125-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'word125', 'https://cdn.learnEase.com/audio/vocab/word125.mp3', NULL, N'["word125a","word125b","word125c"]');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010001-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 1.', 'https://cdn.learnEase.com/audio/exercise/example_001.mp3', N'Example sentence number 1.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010002-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 2.', 'https://cdn.learnEase.com/audio/exercise/example_002.mp3', N'Example sentence number 2.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010003-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 3.', 'https://cdn.learnEase.com/audio/exercise/example_003.mp3', N'Example sentence number 3.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010004-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 4.', 'https://cdn.learnEase.com/audio/exercise/example_004.mp3', N'Example sentence number 4.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010005-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 5.', 'https://cdn.learnEase.com/audio/exercise/example_005.mp3', N'Example sentence number 5.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010006-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 6.', 'https://cdn.learnEase.com/audio/exercise/example_006.mp3', N'Example sentence number 6.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010007-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 7.', 'https://cdn.learnEase.com/audio/exercise/example_007.mp3', N'Example sentence number 7.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010008-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 8.', 'https://cdn.learnEase.com/audio/exercise/example_008.mp3', N'Example sentence number 8.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010009-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 9.', 'https://cdn.learnEase.com/audio/exercise/example_009.mp3', N'Example sentence number 9.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010010-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 10.', 'https://cdn.learnEase.com/audio/exercise/example_010.mp3', N'Example sentence number 10.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010011-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 11.', 'https://cdn.learnEase.com/audio/exercise/example_011.mp3', N'Example sentence number 11.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010012-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 12.', 'https://cdn.learnEase.com/audio/exercise/example_012.mp3', N'Example sentence number 12.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010013-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 13.', 'https://cdn.learnEase.com/audio/exercise/example_013.mp3', N'Example sentence number 13.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010014-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 14.', 'https://cdn.learnEase.com/audio/exercise/example_014.mp3', N'Example sentence number 14.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010015-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 15.', 'https://cdn.learnEase.com/audio/exercise/example_015.mp3', N'Example sentence number 15.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010016-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 16.', 'https://cdn.learnEase.com/audio/exercise/example_016.mp3', N'Example sentence number 16.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010017-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 17.', 'https://cdn.learnEase.com/audio/exercise/example_017.mp3', N'Example sentence number 17.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010018-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 18.', 'https://cdn.learnEase.com/audio/exercise/example_018.mp3', N'Example sentence number 18.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010019-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 19.', 'https://cdn.learnEase.com/audio/exercise/example_019.mp3', N'Example sentence number 19.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010020-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 20.', 'https://cdn.learnEase.com/audio/exercise/example_020.mp3', N'Example sentence number 20.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010021-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 21.', 'https://cdn.learnEase.com/audio/exercise/example_021.mp3', N'Example sentence number 21.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010022-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 22.', 'https://cdn.learnEase.com/audio/exercise/example_022.mp3', N'Example sentence number 22.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010023-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 23.', 'https://cdn.learnEase.com/audio/exercise/example_023.mp3', N'Example sentence number 23.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010024-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 24.', 'https://cdn.learnEase.com/audio/exercise/example_024.mp3', N'Example sentence number 24.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010025-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 25.', 'https://cdn.learnEase.com/audio/exercise/example_025.mp3', N'Example sentence number 25.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010026-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 26.', 'https://cdn.learnEase.com/audio/exercise/example_026.mp3', N'Example sentence number 26.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010027-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 27.', 'https://cdn.learnEase.com/audio/exercise/example_027.mp3', N'Example sentence number 27.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010028-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 28.', 'https://cdn.learnEase.com/audio/exercise/example_028.mp3', N'Example sentence number 28.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010029-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 29.', 'https://cdn.learnEase.com/audio/exercise/example_029.mp3', N'Example sentence number 29.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010030-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 30.', 'https://cdn.learnEase.com/audio/exercise/example_030.mp3', N'Example sentence number 30.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010031-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 31.', 'https://cdn.learnEase.com/audio/exercise/example_031.mp3', N'Example sentence number 31.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010032-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 32.', 'https://cdn.learnEase.com/audio/exercise/example_032.mp3', N'Example sentence number 32.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010033-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 33.', 'https://cdn.learnEase.com/audio/exercise/example_033.mp3', N'Example sentence number 33.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010034-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 34.', 'https://cdn.learnEase.com/audio/exercise/example_034.mp3', N'Example sentence number 34.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010035-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 35.', 'https://cdn.learnEase.com/audio/exercise/example_035.mp3', N'Example sentence number 35.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010036-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 36.', 'https://cdn.learnEase.com/audio/exercise/example_036.mp3', N'Example sentence number 36.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010037-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 37.', 'https://cdn.learnEase.com/audio/exercise/example_037.mp3', N'Example sentence number 37.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010038-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 38.', 'https://cdn.learnEase.com/audio/exercise/example_038.mp3', N'Example sentence number 38.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010039-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 39.', 'https://cdn.learnEase.com/audio/exercise/example_039.mp3', N'Example sentence number 39.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010040-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 40.', 'https://cdn.learnEase.com/audio/exercise/example_040.mp3', N'Example sentence number 40.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010041-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 41.', 'https://cdn.learnEase.com/audio/exercise/example_041.mp3', N'Example sentence number 41.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010042-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 42.', 'https://cdn.learnEase.com/audio/exercise/example_042.mp3', N'Example sentence number 42.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010043-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 43.', 'https://cdn.learnEase.com/audio/exercise/example_043.mp3', N'Example sentence number 43.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010044-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 44.', 'https://cdn.learnEase.com/audio/exercise/example_044.mp3', N'Example sentence number 44.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010045-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 45.', 'https://cdn.learnEase.com/audio/exercise/example_045.mp3', N'Example sentence number 45.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010046-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 46.', 'https://cdn.learnEase.com/audio/exercise/example_046.mp3', N'Example sentence number 46.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010047-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 47.', 'https://cdn.learnEase.com/audio/exercise/example_047.mp3', N'Example sentence number 47.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010048-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 48.', 'https://cdn.learnEase.com/audio/exercise/example_048.mp3', N'Example sentence number 48.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010049-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 49.', 'https://cdn.learnEase.com/audio/exercise/example_049.mp3', N'Example sentence number 49.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010050-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 50.', 'https://cdn.learnEase.com/audio/exercise/example_050.mp3', N'Example sentence number 50.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010051-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 51.', 'https://cdn.learnEase.com/audio/exercise/example_051.mp3', N'Example sentence number 51.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010052-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 52.', 'https://cdn.learnEase.com/audio/exercise/example_052.mp3', N'Example sentence number 52.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010053-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 53.', 'https://cdn.learnEase.com/audio/exercise/example_053.mp3', N'Example sentence number 53.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010054-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 54.', 'https://cdn.learnEase.com/audio/exercise/example_054.mp3', N'Example sentence number 54.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010055-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 55.', 'https://cdn.learnEase.com/audio/exercise/example_055.mp3', N'Example sentence number 55.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010056-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 56.', 'https://cdn.learnEase.com/audio/exercise/example_056.mp3', N'Example sentence number 56.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010057-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 57.', 'https://cdn.learnEase.com/audio/exercise/example_057.mp3', N'Example sentence number 57.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010058-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 58.', 'https://cdn.learnEase.com/audio/exercise/example_058.mp3', N'Example sentence number 58.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010059-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 59.', 'https://cdn.learnEase.com/audio/exercise/example_059.mp3', N'Example sentence number 59.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010060-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 60.', 'https://cdn.learnEase.com/audio/exercise/example_060.mp3', N'Example sentence number 60.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010061-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 61.', 'https://cdn.learnEase.com/audio/exercise/example_061.mp3', N'Example sentence number 61.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010062-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 62.', 'https://cdn.learnEase.com/audio/exercise/example_062.mp3', N'Example sentence number 62.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010063-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 63.', 'https://cdn.learnEase.com/audio/exercise/example_063.mp3', N'Example sentence number 63.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010064-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 64.', 'https://cdn.learnEase.com/audio/exercise/example_064.mp3', N'Example sentence number 64.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010065-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 65.', 'https://cdn.learnEase.com/audio/exercise/example_065.mp3', N'Example sentence number 65.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010066-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 66.', 'https://cdn.learnEase.com/audio/exercise/example_066.mp3', N'Example sentence number 66.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010067-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 67.', 'https://cdn.learnEase.com/audio/exercise/example_067.mp3', N'Example sentence number 67.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010068-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 68.', 'https://cdn.learnEase.com/audio/exercise/example_068.mp3', N'Example sentence number 68.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010069-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 69.', 'https://cdn.learnEase.com/audio/exercise/example_069.mp3', N'Example sentence number 69.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010070-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 70.', 'https://cdn.learnEase.com/audio/exercise/example_070.mp3', N'Example sentence number 70.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010071-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 71.', 'https://cdn.learnEase.com/audio/exercise/example_071.mp3', N'Example sentence number 71.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010072-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 72.', 'https://cdn.learnEase.com/audio/exercise/example_072.mp3', N'Example sentence number 72.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010073-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 73.', 'https://cdn.learnEase.com/audio/exercise/example_073.mp3', N'Example sentence number 73.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010074-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 74.', 'https://cdn.learnEase.com/audio/exercise/example_074.mp3', N'Example sentence number 74.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010075-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 75.', 'https://cdn.learnEase.com/audio/exercise/example_075.mp3', N'Example sentence number 75.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010076-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 76.', 'https://cdn.learnEase.com/audio/exercise/example_076.mp3', N'Example sentence number 76.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010077-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 77.', 'https://cdn.learnEase.com/audio/exercise/example_077.mp3', N'Example sentence number 77.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010078-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 78.', 'https://cdn.learnEase.com/audio/exercise/example_078.mp3', N'Example sentence number 78.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010079-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 79.', 'https://cdn.learnEase.com/audio/exercise/example_079.mp3', N'Example sentence number 79.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010080-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 80.', 'https://cdn.learnEase.com/audio/exercise/example_080.mp3', N'Example sentence number 80.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010081-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 81.', 'https://cdn.learnEase.com/audio/exercise/example_081.mp3', N'Example sentence number 81.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010082-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 82.', 'https://cdn.learnEase.com/audio/exercise/example_082.mp3', N'Example sentence number 82.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010083-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 83.', 'https://cdn.learnEase.com/audio/exercise/example_083.mp3', N'Example sentence number 83.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010084-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 84.', 'https://cdn.learnEase.com/audio/exercise/example_084.mp3', N'Example sentence number 84.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010085-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 85.', 'https://cdn.learnEase.com/audio/exercise/example_085.mp3', N'Example sentence number 85.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010086-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 86.', 'https://cdn.learnEase.com/audio/exercise/example_086.mp3', N'Example sentence number 86.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010087-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 87.', 'https://cdn.learnEase.com/audio/exercise/example_087.mp3', N'Example sentence number 87.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010088-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 88.', 'https://cdn.learnEase.com/audio/exercise/example_088.mp3', N'Example sentence number 88.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010089-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 89.', 'https://cdn.learnEase.com/audio/exercise/example_089.mp3', N'Example sentence number 89.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010090-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 90.', 'https://cdn.learnEase.com/audio/exercise/example_090.mp3', N'Example sentence number 90.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010091-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 91.', 'https://cdn.learnEase.com/audio/exercise/example_091.mp3', N'Example sentence number 91.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010092-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 92.', 'https://cdn.learnEase.com/audio/exercise/example_092.mp3', N'Example sentence number 92.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010093-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 93.', 'https://cdn.learnEase.com/audio/exercise/example_093.mp3', N'Example sentence number 93.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010094-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 94.', 'https://cdn.learnEase.com/audio/exercise/example_094.mp3', N'Example sentence number 94.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010095-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 95.', 'https://cdn.learnEase.com/audio/exercise/example_095.mp3', N'Example sentence number 95.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010096-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 96.', 'https://cdn.learnEase.com/audio/exercise/example_096.mp3', N'Example sentence number 96.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010097-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 97.', 'https://cdn.learnEase.com/audio/exercise/example_097.mp3', N'Example sentence number 97.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010098-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 98.', 'https://cdn.learnEase.com/audio/exercise/example_098.mp3', N'Example sentence number 98.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010099-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 99.', 'https://cdn.learnEase.com/audio/exercise/example_099.mp3', N'Example sentence number 99.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010100-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 100.', 'https://cdn.learnEase.com/audio/exercise/example_100.mp3', N'Example sentence number 100.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010101-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 101.', 'https://cdn.learnEase.com/audio/exercise/example_101.mp3', N'Example sentence number 101.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010102-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 102.', 'https://cdn.learnEase.com/audio/exercise/example_102.mp3', N'Example sentence number 102.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010103-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 103.', 'https://cdn.learnEase.com/audio/exercise/example_103.mp3', N'Example sentence number 103.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010104-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 104.', 'https://cdn.learnEase.com/audio/exercise/example_104.mp3', N'Example sentence number 104.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010105-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 105.', 'https://cdn.learnEase.com/audio/exercise/example_105.mp3', N'Example sentence number 105.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010106-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 106.', 'https://cdn.learnEase.com/audio/exercise/example_106.mp3', N'Example sentence number 106.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010107-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 107.', 'https://cdn.learnEase.com/audio/exercise/example_107.mp3', N'Example sentence number 107.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010108-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 108.', 'https://cdn.learnEase.com/audio/exercise/example_108.mp3', N'Example sentence number 108.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010109-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 109.', 'https://cdn.learnEase.com/audio/exercise/example_109.mp3', N'Example sentence number 109.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010110-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 110.', 'https://cdn.learnEase.com/audio/exercise/example_110.mp3', N'Example sentence number 110.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010111-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 111.', 'https://cdn.learnEase.com/audio/exercise/example_111.mp3', N'Example sentence number 111.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010112-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 112.', 'https://cdn.learnEase.com/audio/exercise/example_112.mp3', N'Example sentence number 112.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010113-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 113.', 'https://cdn.learnEase.com/audio/exercise/example_113.mp3', N'Example sentence number 113.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010114-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 114.', 'https://cdn.learnEase.com/audio/exercise/example_114.mp3', N'Example sentence number 114.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010115-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 115.', 'https://cdn.learnEase.com/audio/exercise/example_115.mp3', N'Example sentence number 115.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010116-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 116.', 'https://cdn.learnEase.com/audio/exercise/example_116.mp3', N'Example sentence number 116.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010117-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 117.', 'https://cdn.learnEase.com/audio/exercise/example_117.mp3', N'Example sentence number 117.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010118-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 118.', 'https://cdn.learnEase.com/audio/exercise/example_118.mp3', N'Example sentence number 118.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010119-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 119.', 'https://cdn.learnEase.com/audio/exercise/example_119.mp3', N'Example sentence number 119.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010120-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 120.', 'https://cdn.learnEase.com/audio/exercise/example_120.mp3', N'Example sentence number 120.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010121-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 121.', 'https://cdn.learnEase.com/audio/exercise/example_121.mp3', N'Example sentence number 121.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010122-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 122.', 'https://cdn.learnEase.com/audio/exercise/example_122.mp3', N'Example sentence number 122.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010123-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 123.', 'https://cdn.learnEase.com/audio/exercise/example_123.mp3', N'Example sentence number 123.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010124-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 124.', 'https://cdn.learnEase.com/audio/exercise/example_124.mp3', N'Example sentence number 124.');
INSERT INTO SpeakingExercises (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText) VALUES ('E0010125-0000-0000-0000-000000000000', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'Say: Example sentence number 125.', 'https://cdn.learnEase.com/audio/exercise/example_125.mp3', N'Example sentence number 125.');

/*========================================================
  6. ACHIEVEMENT & LEADERBOARD
========================================================*/
INSERT INTO Achievements
  (AchievementId, UserId, Name, Description, AchievedAt)
VALUES
  ('BBCCDD11-22EE-33FF-44AA-556677889900',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   N'First Lesson Completed',
   N'Hoàn thành bài học đầu tiên',
   DATEADD(HOUR,-2,SYSUTCDATETIME()));

INSERT INTO Leaderboards
  (LeaderboardId, UserId, Period, Score, RecordedAt)
VALUES
  ('CCDDEEFF-1122-3344-5566-77889900AABB',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   'weekly', 200,
   SYSUTCDATETIME());

/*========================================================
  7. AiLessons & AiLessonParts
========================================================*/
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'a1dcd9b7-6db7-4270-b195-0eb536175289', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:26:43.7475610' AS DateTime2), 2)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:28.2168449' AS DateTime2), 6)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:37.3671289' AS DateTime2), 7)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:21.4634465' AS DateTime2), 5)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:01.2432926' AS DateTime2), 3)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'ae447797-ef7d-400e-b862-83f6c2eef80d', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:10.7891748' AS DateTime2), 4)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:26:30.8610478' AS DateTime2), 1)
GO
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'1babd4ba-1c8d-4cab-a622-104be8371424', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 0, N'Listen to the audio and type exactly what you hear.', N'she usually reads books, but not today', N'https://storage.googleapis.com/learnease/d442bdba-8ac1-44fb-afb1-ccb36c65464c_da6239c5-976e-4439-88f0-31e656ac4007.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'de517212-5d7e-4612-bf6e-11bcc055da81', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 1, N'what activities do you usually do now versus weekends?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'924df427-51e3-478d-bba7-2471b6c9a8f5', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 2, N'Read the passage and answer the questions.', N'a2–b1 learners often compare present simple and present continuous tenses in english. present simple is used for regular actions or general truths, while present continuous is used for actions happening now. for example, ''i study english every day'' (present simple) versus ''i am studying english right now'' (present continuous).', NULL, N'[
    {
      "question": "What is Present Simple used for?",
      "choices": ["A. Regular actions or general truths", "B. Actions happening now", "C. Past actions", "D. Future actions"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now?",
      "choices": ["A. Present Simple", "B. Present Continuous", "C. Past Perfect", "D. Future Perfect"],
      "answer": "B"
    },
    {
      "question": "In the sentence ''I am studying English right now'', which tense is being used?",
      "choices": ["A. Present Simple", "B. Present Continuous", "C. Past Simple", "D. Future Simple"],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'75dd6b0f-d568-4836-a382-25cdabe9fb56', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 2, N'Read the passage and answer the questions.', N'a common challenge for english learners is understanding the difference between the present simple and present continuous tenses. the present simple is used for habits and routines, while the present continuous is used for actions happening now. for example, ''she eats fruit every day'' (present simple) and ''she is eating an apple right now'' (present continuous) showcase this contrast.', NULL, N'[
{"question": "When is the Present Simple tense typically used?", "choices": ["A. For habits and routines", "B. For actions happening now", "C. For future plans", "D. For past events"], "answer": "A"},
{"question": "Which tense is suitable for describing actions happening now?", "choices": ["A. Present Simple", "B. Past Simple", "C. Present Continuous", "D. Past Continuous"], "answer": "C"},
{"question": "In the sentence ''He ___ a book every night'', which tense should be used to fill in the blank?", "choices": ["A. reads", "B. is reading", "C. read", "D. reading"], "answer": "A"}
]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'31719140-1537-4bd6-81bb-29894a174278', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 2, N'Read the passage and answer the questions.', N'a2-b1 learners often struggle with differentiating between the present simple and present continuous tenses in english. the present simple is used for habits and routines, while the present continuous is used for actions happening now. remember, the present simple is like a timetable and the present continuous is like a live broadcast.', NULL, N'[
    {
      "question": "When do we use the Present Simple tense?",
      "choices": ["A. For actions happening now", "B. For habits and routines", "C. For future plans", "D. For completed actions in the past"],
      "answer": "B"
    },
    {
      "question": "What is a characteristic of the Present Continuous tense?",
      "choices": ["A. Talking about general truths", "B. Describing past events", "C. Expressing future intentions", "D. Describing actions happening now"],
      "answer": "D"
    },
    {
      "question": "Which comparison best describes the difference between Present Simple and Present Continuous tenses?",
      "choices": ["A. Present Simple is continuous, Present Continuous is simple", "B. Present Simple is like a timetable, Present Continuous is like a live broadcast", "C. Present Simple is used for future actions, Present Continuous is used for past actions", "D. Present Simple is used for singular subjects, Present Continuous is used for plural subjects"],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'6cfc15cb-040f-48b0-aaec-2c3999734067', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 3, N'describe a typical weekday using both present simple and present continuous tenses', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'3fcd59f9-ff3f-43e4-bad9-3972a29ae5e7', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 1, N'what are you doing now? compare with your usual routine', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'0eb936e5-bd8d-4019-b25f-3f6338b6b967', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 3, N'describe your morning routine using both present simple and present continuous tenses', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'02bca432-d783-4028-beb0-49a5f2184c7b', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 3, N'describe your daily routine in present simple tense, then switch to present continuous to highlight temporary actions', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'1e9c072b-b6f5-44c2-b163-4cd28282360d', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 0, N'Listen to the audio and type exactly what you hear.', N'she often reads in the library, but today she''s dancing', N'https://storage.googleapis.com/learnease/aa92ff78-da5e-4f1b-8406-238e2989f819_d8be2697-e735-47f3-ada9-dc1fa078a124.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'967884a2-689b-4a0c-8d71-4fb2ef173e47', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 3, N'describe a typical day using the present simple, then rewrite it in the present continuous tense', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'29e04726-1e65-48bb-a8db-50b98de5b440', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 1, N'what is john doing now compared to what he usually does?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'2a348213-6e1e-4202-90b1-7451bab6f328', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 0, N'Listen to the audio and type exactly what you hear.', N'he usually watches tv, but now he is studying', N'https://storage.googleapis.com/learnease/116af24b-4b23-4a66-99a4-cb6a248d2d8b_d8213d46-c68a-477a-96be-a1ef9ccdd481.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'3731ec00-b60b-4d24-935d-752e6441ebad', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 2, N'Read the passage and answer the questions.', N'the present simple is used for habits and routines while the present continuous is used for actions happening now. for example, ''i eat breakfast every day'' (present simple) and ''i am eating lunch right now'' (present continuous). understanding these differences helps improve communication skills in english.', NULL, N'[
    {
      "question": "Which tense is used for habits and routines?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Past Continuous"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Past Continuous"],
      "answer": "B"
    },
    {
      "question": "In which sentence is the Present Continuous tense correctly used?",
      "choices": ["A) She runs in the park every morning.", "B) They are reading a book at the moment.", "C) I drink coffee in the evening.", "D) He played football yesterday."],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'f2ea031c-fed0-4cfb-ba6b-7cc909f62931', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 3, N'describe your daily routine using the present simple tense, then switch to the present continuous to highlight specific actions', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'b0132430-5f83-40e0-8232-85e6094545d3', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 1, N'what activities do you usually do vs. are doing today?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'edef1025-b70d-491d-913d-8e78d701ea3b', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 0, N'Listen to the audio and type exactly what you hear.', N'she usually reads books, but today she is watching tv', N'https://storage.googleapis.com/learnease/864b1837-114e-4020-b72a-a95689239d4f_6f52597d-1485-4bb0-9144-9133e4bde71d.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'7f422ada-e7c1-46cd-b82b-a3081a0bd3a5', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 3, N'describe your daily routine using the present simple tense, then switch to the present continuous to highlight ongoing actions', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'6dcfc923-b9d4-4e4b-9f25-a661efbf2035', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 2, N'Read the passage and answer the questions.', N'a2-b1 learners often struggle with the differences between present simple and present continuous tenses. present simple is used for routine actions or facts, while present continuous is for actions happening now or temporary situations. practice is key to mastering these tenses.', NULL, N'[
    {
      "question": "Which tense is used for routine actions or facts?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Past Continuous"],
      "answer": "A"
    },
    {
      "question": "When do we use Present Continuous tense?",
      "choices": ["A) For actions happening now", "B) For routine actions", "C) For past events", "D) For permanent situations"],
      "answer": "A"
    },
    {
      "question": "What is essential to master Present Simple and Present Continuous tenses?",
      "choices": ["A) Consistent practice", "B) Random guessing", "C) Ignoring grammar rules", "D) Speaking in a different language"],
      "answer": "A"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'faaaea65-f814-4d6a-8916-aafd2dfa9033', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 3, N'describe what you are doing now and your daily routine in both the present simple and present continuous tenses', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'02da7d37-af39-42be-90d1-b36e119aa757', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 2, N'Read the passage and answer the questions.', N'a2–b1 learners often confuse the present simple and present continuous tenses in english. the present simple is used for routines and habits, while the present continuous is used for actions happening now. remember, ''i play tennis'' (present simple) vs. ''i am playing tennis'' (present continuous). practice using both tenses to improve your english skills.', NULL, N'[
    {
      "question": "Which tense is used for routines and habits?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Future Simple"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Future Simple"],
      "answer": "B"
    },
    {
      "question": "Choose the correct sentence using the Present Continuous tense:",
      "choices": ["A) He runs every morning.", "B) They are playing football at the moment.", "C) She doesn''t like bananas.", "D) We will go to the beach tomorrow."],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'637a7d31-df4d-4924-848e-bc9a8944ca36', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 0, N'Listen to the audio and type exactly what you hear.', N'she always reads in the library but is studying now', N'https://storage.googleapis.com/learnease/e5ae0758-2b1e-4e05-9210-97b913de09a3_2977240e-387f-4a04-b33f-b0cbec835c89.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'a7450034-c2fe-4833-8376-caa680226eb0', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 0, N'Listen to the audio and type exactly what you hear.', N'she often reads books, but today she is watching a movie', N'https://storage.googleapis.com/learnease/99fa4918-2e28-472f-abad-13fc01595f74_a8ca1ca5-3ef2-4349-9738-9ee2ef2db9b7.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'99cff458-cfb1-4ae1-810d-e0fed8f0dc2f', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 0, N'Listen to the audio and type exactly what you hear.', N'she always paints on sundays, but she is drawing tonight', N'https://storage.googleapis.com/learnease/c96aeab9-5ec7-40da-ae29-0f4276838149_cf0b3697-0f5c-4e0b-a50a-0349769951bd.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'2afaf492-f3af-43af-a393-e83ed295d83a', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 1, N'what is the difference between "i play" and "i am playing"?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'89dfe735-ed1f-473e-8f69-ebffdb2cadae', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 2, N'Read the passage and answer the questions.', N'a2-b1 learners often struggle with the differences between the present simple and present continuous tenses in english. the present simple is used for habitual actions or general truths, while the present continuous is used for actions happening now or future arrangements. it''s important to pay attention to keywords like ''always'' for present simple and ''now'' for present continuous.', NULL, N'[
    {
      "question": "Which tense is used for habitual actions or general truths?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Future Simple"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now or future arrangements?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Continuous", "D) Future Simple"],
      "answer": "B"
    },
    {
      "question": "What kind of keywords should learners pay attention to when using the Present Continuous tense?",
      "choices": ["A) Past tense keywords", "B) Future tense keywords", "C) Continuous tense keywords", "D) ''now'' and ''future'' keywords"],
      "answer": "D"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'44a2f0a5-6871-4067-a48f-f867cd5b0172', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 1, N'when do you use present simple and present continuous tenses?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'8c84d4a8-53c4-47b1-91f0-fcc7ade6b6c8', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 1, N'do you prefer reading books or watching movies in english?', NULL, NULL, NULL)
GO

/*========================================================
  8. TOPIC
========================================================*/
INSERT INTO Topic (TopicId, Title, Description, [Order])
VALUES
  ('11111111-1111-1111-1111-111111111111', N'Greetings', N'Learn how to greet people in different situations.', 1),
  ('22222222-2222-2222-2222-222222222222', N'Daily Routine', N'Vocabulary and speaking related to everyday daily habits.', 2),
  ('33333333-3333-3333-3333-333333333333', N'Food & Drink', N'Words and phrases for food, meals, eating out and drinks.', 3),
  ('44444444-4444-4444-4444-444444444444', N'Work & Office', N'Essential vocabulary for professional and workplace communication.', 4),
  ('55555555-5555-5555-5555-555555555555', N'Travel', N'Language and expressions for traveling, directions and transportation.', 5);
  /*========================================================
  9. LESSON (5 lesson mỗi topic)
========================================================*/
INSERT INTO Lessons(LessonId, TopicId, DialectId, [Order], Title)
VALUES
-- Greetings Topic (ID: 1111...)
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Greeting Basics'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Formal Greetings'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Casual Greetings'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Introducing Yourself'),
('0F63CEAA-1BDF-4921-987F-72F07B11A95E', '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Farewells'),

-- Daily Routine Topic (ID: 2222...)
('69AD1BC5-3637-4877-BEC0-A3CDDEB0E5CD', '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Morning Activities'),
('0A019DB5-729C-426F-9B2C-4F95E2231205', '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Daily Chores'),
('31E9E5D2-6844-40A9-B407-C0A3113B5565', '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Work and Study'),
('49509115-D2E5-467B-968A-22B30A75082F', '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Evening Routine'),
('430E03AC-C1F2-48EE-A685-C65491EE6640', '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Weekend Activities'),

-- Food & Drink Topic (ID: 3333...)
('5AF715BA-FEDF-46E6-BAF0-824AC4798CA3', '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Common Foods'),
('65DF01C1-5E66-4C01-9427-BD2DBBFE3277', '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Drinks and Beverages'),
('EC86E519-5A9A-41AB-8BF4-EA0A68099441', '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'At the Restaurant'),
('AF9F67BC-3477-4729-B913-226AA5486715', '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Likes and Dislikes'),
('2636C6FC-9A72-4DB1-B5F0-FF533EFB0A64', '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Food Quantities'),

-- Work & Office Topic (ID: 4444...)
('E2080D2E-6BA8-4A18-817F-A2B8CD407AEB', '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Office Vocabulary'),
('C8BA22F8-4E40-4F5D-B3E6-434943B1B628', '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Meeting Language'),
('F83B6646-8948-4DAD-8436-C8F5DDEA5211', '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Writing Emails'),
('3FA1C169-FE55-47B2-AE4D-E8D68B818A3D', '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Job Interviews'),
('1FADED40-027A-4157-BD8A-91CDE2483FB1', '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Describing Jobs'),

-- Travel Topic (ID: 5555...)
('3764133A-0D63-4F63-A3A0-3EA8DADEF04D', '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'At the Airport'),
('52E42A41-7BDA-4FE5-85E2-7BD6B1D5C619', '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Hotel Conversations'),
('B0798ED3-DF09-448F-84DF-0DF762C44FFC', '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Asking for Directions'),
('C6B6D2B4-26EC-4D41-ADC7-1B10A95798CE', '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Transportation'),
('86C9B06D-8774-4AE7-A6E4-89DF016D0627', '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Travel Problems');


-- INSERT INTO Lessons(LessonId, TopicId, DialectId, [Order], Title)
-- VALUES
-- -- Greetings Topic (ID: 1111...)
-- (NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Greeting Basics'),
-- (NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Formal Greetings'),
-- (NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Casual Greetings'),
-- (NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Introducing Yourself'),
-- (NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Farewells'),

-- -- Daily Routine Topic (ID: 2222...)
-- (NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Morning Activities'),
-- (NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Daily Chores'),
-- (NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Work and Study'),
-- (NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Evening Routine'),
-- (NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Weekend Activities'),

-- -- Food & Drink Topic (ID: 3333...)
-- (NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Common Foods'),
-- (NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Drinks and Beverages'),
-- (NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'At the Restaurant'),
-- (NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Likes and Dislikes'),
-- (NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Food Quantities'),

-- -- Work & Office Topic (ID: 4444...)
-- (NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Office Vocabulary'),
-- (NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Meeting Language'),
-- (NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Writing Emails'),
-- (NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Job Interviews'),
-- (NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Describing Jobs'),

-- -- Travel Topic (ID: 5555...)
-- (NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'At the Airport'),
-- (NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Hotel Conversations'),
-- (NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Asking for Directions'),
-- (NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Transportation'),
-- (NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Travel Problems');
INSERT INTO LessonVocabularies (LessonId, VocabId) VALUES 
-- Lesson 1
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'C0010001-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'C0010002-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'C0010003-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'C0010004-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'C0010005-0000-0000-0000-000000000000'),

-- Lesson 2
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'C0010006-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'C0010007-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'C0010008-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'C0010009-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'C0010010-0000-0000-0000-000000000000'),

-- Lesson 3
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'C0010011-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'C0010012-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'C0010013-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'C0010014-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'C0010015-0000-0000-0000-000000000000'),

-- Lesson 4
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'C0010016-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'C0010017-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'C0010018-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'C0010019-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'C0010020-0000-0000-0000-000000000000');

INSERT INTO LessonSpeakings (LessonId, ExerciseId) VALUES
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'E0010001-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'E0010002-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'E0010003-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'E0010004-0000-0000-0000-000000000000'),
('981DA0B3-C785-45E9-9FD8-AF9E5BB64FB5', 'E0010005-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'E0010006-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'E0010007-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'E0010008-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'E0010009-0000-0000-0000-000000000000'),
('B53D0603-85F1-44EC-853E-FDE2E5B2DB02', 'E0010010-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'E0010011-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'E0010012-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'E0010013-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'E0010014-0000-0000-0000-000000000000'),
('94C8C998-F2A6-4066-928A-BA7CACCAA74D', 'E0010015-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'E0010016-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'E0010017-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'E0010018-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'E0010019-0000-0000-0000-000000000000'),
('5EE61239-9FDF-4E7F-9D0F-3B6FF45E63DE', 'E0010020-0000-0000-0000-000000000000'),
('0F63CEAA-1BDF-4921-987F-72F07B11A95E', 'E0010021-0000-0000-0000-000000000000'),
('0F63CEAA-1BDF-4921-987F-72F07B11A95E', 'E0010022-0000-0000-0000-000000000000'),
('0F63CEAA-1BDF-4921-987F-72F07B11A95E', 'E0010023-0000-0000-0000-000000000000'),
('0F63CEAA-1BDF-4921-987F-72F07B11A95E', 'E0010024-0000-0000-0000-000000000000'),
('0F63CEAA-1BDF-4921-987F-72F07B11A95E', 'E0010025-0000-0000-0000-000000000000'),
('69AD1BC5-3637-4877-BEC0-A3CDDEB0E5CD', 'E0010026-0000-0000-0000-000000000000'),
('69AD1BC5-3637-4877-BEC0-A3CDDEB0E5CD', 'E0010027-0000-0000-0000-000000000000'),
('69AD1BC5-3637-4877-BEC0-A3CDDEB0E5CD', 'E0010028-0000-0000-0000-000000000000'),
('69AD1BC5-3637-4877-BEC0-A3CDDEB0E5CD', 'E0010029-0000-0000-0000-000000000000'),
('69AD1BC5-3637-4877-BEC0-A3CDDEB0E5CD', 'E0010030-0000-0000-0000-000000000000'),
('0A019DB5-729C-426F-9B2C-4F95E2231205', 'E0010031-0000-0000-0000-000000000000'),
('0A019DB5-729C-426F-9B2C-4F95E2231205', 'E0010032-0000-0000-0000-000000000000'),
('0A019DB5-729C-426F-9B2C-4F95E2231205', 'E0010033-0000-0000-0000-000000000000'),
('0A019DB5-729C-426F-9B2C-4F95E2231205', 'E0010034-0000-0000-0000-000000000000'),
('0A019DB5-729C-426F-9B2C-4F95E2231205', 'E0010035-0000-0000-0000-000000000000'),
('31E9E5D2-6844-40A9-B407-C0A3113B5565', 'E0010036-0000-0000-0000-000000000000'),
('31E9E5D2-6844-40A9-B407-C0A3113B5565', 'E0010037-0000-0000-0000-000000000000'),
('31E9E5D2-6844-40A9-B407-C0A3113B5565', 'E0010038-0000-0000-0000-000000000000'),
('31E9E5D2-6844-40A9-B407-C0A3113B5565', 'E0010039-0000-0000-0000-000000000000'),
('31E9E5D2-6844-40A9-B407-C0A3113B5565', 'E0010040-0000-0000-0000-000000000000'),
('49509115-D2E5-467B-968A-22B30A75082F', 'E0010041-0000-0000-0000-000000000000'),
('49509115-D2E5-467B-968A-22B30A75082F', 'E0010042-0000-0000-0000-000000000000'),
('49509115-D2E5-467B-968A-22B30A75082F', 'E0010043-0000-0000-0000-000000000000'),
('49509115-D2E5-467B-968A-22B30A75082F', 'E0010044-0000-0000-0000-000000000000'),
('49509115-D2E5-467B-968A-22B30A75082F', 'E0010045-0000-0000-0000-000000000000'),
('430E03AC-C1F2-48EE-A685-C65491EE6640', 'E0010046-0000-0000-0000-000000000000'),
('430E03AC-C1F2-48EE-A685-C65491EE6640', 'E0010047-0000-0000-0000-000000000000'),
('430E03AC-C1F2-48EE-A685-C65491EE6640', 'E0010048-0000-0000-0000-000000000000'),
('430E03AC-C1F2-48EE-A685-C65491EE6640', 'E0010049-0000-0000-0000-000000000000'),
('430E03AC-C1F2-48EE-A685-C65491EE6640', 'E0010050-0000-0000-0000-000000000000'),
('5AF715BA-FEDF-46E6-BAF0-824AC4798CA3', 'E0010051-0000-0000-0000-000000000000'),
('5AF715BA-FEDF-46E6-BAF0-824AC4798CA3', 'E0010052-0000-0000-0000-000000000000'),
('5AF715BA-FEDF-46E6-BAF0-824AC4798CA3', 'E0010053-0000-0000-0000-000000000000'),
('5AF715BA-FEDF-46E6-BAF0-824AC4798CA3', 'E0010054-0000-0000-0000-000000000000'),
('5AF715BA-FEDF-46E6-BAF0-824AC4798CA3', 'E0010055-0000-0000-0000-000000000000'),
('65DF01C1-5E66-4C01-9427-BD2DBBFE3277', 'E0010056-0000-0000-0000-000000000000'),
('65DF01C1-5E66-4C01-9427-BD2DBBFE3277', 'E0010057-0000-0000-0000-000000000000'),
('65DF01C1-5E66-4C01-9427-BD2DBBFE3277', 'E0010058-0000-0000-0000-000000000000'),
('65DF01C1-5E66-4C01-9427-BD2DBBFE3277', 'E0010059-0000-0000-0000-000000000000'),
('65DF01C1-5E66-4C01-9427-BD2DBBFE3277', 'E0010060-0000-0000-0000-000000000000'),
('EC86E519-5A9A-41AB-8BF4-EA0A68099441', 'E0010061-0000-0000-0000-000000000000'),
('EC86E519-5A9A-41AB-8BF4-EA0A68099441', 'E0010062-0000-0000-0000-000000000000'),
('EC86E519-5A9A-41AB-8BF4-EA0A68099441', 'E0010063-0000-0000-0000-000000000000'),
('EC86E519-5A9A-41AB-8BF4-EA0A68099441', 'E0010064-0000-0000-0000-000000000000'),
('EC86E519-5A9A-41AB-8BF4-EA0A68099441', 'E0010065-0000-0000-0000-000000000000'),
('AF9F67BC-3477-4729-B913-226AA5486715', 'E0010066-0000-0000-0000-000000000000'),
('AF9F67BC-3477-4729-B913-226AA5486715', 'E0010067-0000-0000-0000-000000000000'),
('AF9F67BC-3477-4729-B913-226AA5486715', 'E0010068-0000-0000-0000-000000000000'),
('AF9F67BC-3477-4729-B913-226AA5486715', 'E0010069-0000-0000-0000-000000000000'),
('AF9F67BC-3477-4729-B913-226AA5486715', 'E0010070-0000-0000-0000-000000000000'),
('2636C6FC-9A72-4DB1-B5F0-FF533EFB0A64', 'E0010071-0000-0000-0000-000000000000'),
('2636C6FC-9A72-4DB1-B5F0-FF533EFB0A64', 'E0010072-0000-0000-0000-000000000000'),
('2636C6FC-9A72-4DB1-B5F0-FF533EFB0A64', 'E0010073-0000-0000-0000-000000000000'),
('2636C6FC-9A72-4DB1-B5F0-FF533EFB0A64', 'E0010074-0000-0000-0000-000000000000'),
('2636C6FC-9A72-4DB1-B5F0-FF533EFB0A64', 'E0010075-0000-0000-0000-000000000000'),
('E2080D2E-6BA8-4A18-817F-A2B8CD407AEB', 'E0010076-0000-0000-0000-000000000000'),
('E2080D2E-6BA8-4A18-817F-A2B8CD407AEB', 'E0010077-0000-0000-0000-000000000000'),
('E2080D2E-6BA8-4A18-817F-A2B8CD407AEB', 'E0010078-0000-0000-0000-000000000000'),
('E2080D2E-6BA8-4A18-817F-A2B8CD407AEB', 'E0010079-0000-0000-0000-000000000000'),
('E2080D2E-6BA8-4A18-817F-A2B8CD407AEB', 'E0010080-0000-0000-0000-000000000000'),
('C8BA22F8-4E40-4F5D-B3E6-434943B1B628', 'E0010081-0000-0000-0000-000000000000'),
('C8BA22F8-4E40-4F5D-B3E6-434943B1B628', 'E0010082-0000-0000-0000-000000000000'),
('C8BA22F8-4E40-4F5D-B3E6-434943B1B628', 'E0010083-0000-0000-0000-000000000000'),
('C8BA22F8-4E40-4F5D-B3E6-434943B1B628', 'E0010084-0000-0000-0000-000000000000'),
('C8BA22F8-4E40-4F5D-B3E6-434943B1B628', 'E0010085-0000-0000-0000-000000000000'),
('F83B6646-8948-4DAD-8436-C8F5DDEA5211', 'E0010086-0000-0000-0000-000000000000'),
('F83B6646-8948-4DAD-8436-C8F5DDEA5211', 'E0010087-0000-0000-0000-000000000000'),
('F83B6646-8948-4DAD-8436-C8F5DDEA5211', 'E0010088-0000-0000-0000-000000000000'),
('F83B6646-8948-4DAD-8436-C8F5DDEA5211', 'E0010089-0000-0000-0000-000000000000'),
('F83B6646-8948-4DAD-8436-C8F5DDEA5211', 'E0010090-0000-0000-0000-000000000000'),
('3FA1C169-FE55-47B2-AE4D-E8D68B818A3D', 'E0010091-0000-0000-0000-000000000000'),
('3FA1C169-FE55-47B2-AE4D-E8D68B818A3D', 'E0010092-0000-0000-0000-000000000000'),
('3FA1C169-FE55-47B2-AE4D-E8D68B818A3D', 'E0010093-0000-0000-0000-000000000000'),
('3FA1C169-FE55-47B2-AE4D-E8D68B818A3D', 'E0010094-0000-0000-0000-000000000000'),
('3FA1C169-FE55-47B2-AE4D-E8D68B818A3D', 'E0010095-0000-0000-0000-000000000000'),
('1FADED40-027A-4157-BD8A-91CDE2483FB1', 'E0010096-0000-0000-0000-000000000000'),
('1FADED40-027A-4157-BD8A-91CDE2483FB1', 'E0010097-0000-0000-0000-000000000000'),
('1FADED40-027A-4157-BD8A-91CDE2483FB1', 'E0010098-0000-0000-0000-000000000000'),
('1FADED40-027A-4157-BD8A-91CDE2483FB1', 'E0010099-0000-0000-0000-000000000000'),
('1FADED40-027A-4157-BD8A-91CDE2483FB1', 'E0010100-0000-0000-0000-000000000000'),
('3764133A-0D63-4F63-A3A0-3EA8DADEF04D', 'E0010101-0000-0000-0000-000000000000'),
('3764133A-0D63-4F63-A3A0-3EA8DADEF04D', 'E0010102-0000-0000-0000-000000000000'),
('3764133A-0D63-4F63-A3A0-3EA8DADEF04D', 'E0010103-0000-0000-0000-000000000000'),
('3764133A-0D63-4F63-A3A0-3EA8DADEF04D', 'E0010104-0000-0000-0000-000000000000'),
('3764133A-0D63-4F63-A3A0-3EA8DADEF04D', 'E0010105-0000-0000-0000-000000000000'),
('52E42A41-7BDA-4FE5-85E2-7BD6B1D5C619', 'E0010106-0000-0000-0000-000000000000'),
('52E42A41-7BDA-4FE5-85E2-7BD6B1D5C619', 'E0010107-0000-0000-0000-000000000000'),
('52E42A41-7BDA-4FE5-85E2-7BD6B1D5C619', 'E0010108-0000-0000-0000-000000000000'),
('52E42A41-7BDA-4FE5-85E2-7BD6B1D5C619', 'E0010109-0000-0000-0000-000000000000'),
('52E42A41-7BDA-4FE5-85E2-7BD6B1D5C619', 'E0010110-0000-0000-0000-000000000000'),
('B0798ED3-DF09-448F-84DF-0DF762C44FFC', 'E0010111-0000-0000-0000-000000000000'),
('B0798ED3-DF09-448F-84DF-0DF762C44FFC', 'E0010112-0000-0000-0000-000000000000'),
('B0798ED3-DF09-448F-84DF-0DF762C44FFC', 'E0010113-0000-0000-0000-000000000000'),
('B0798ED3-DF09-448F-84DF-0DF762C44FFC', 'E0010114-0000-0000-0000-000000000000'),
('B0798ED3-DF09-448F-84DF-0DF762C44FFC', 'E0010115-0000-0000-0000-000000000000'),
('C6B6D2B4-26EC-4D41-ADC7-1B10A95798CE', 'E0010116-0000-0000-0000-000000000000'),
('C6B6D2B4-26EC-4D41-ADC7-1B10A95798CE', 'E0010117-0000-0000-0000-000000000000'),
('C6B6D2B4-26EC-4D41-ADC7-1B10A95798CE', 'E0010118-0000-0000-0000-000000000000'),
('C6B6D2B4-26EC-4D41-ADC7-1B10A95798CE', 'E0010119-0000-0000-0000-000000000000'),
('C6B6D2B4-26EC-4D41-ADC7-1B10A95798CE', 'E0010120-0000-0000-0000-000000000000'),
('86C9B06D-8774-4AE7-A6E4-89DF016D0627', 'E0010121-0000-0000-0000-000000000000'),
('86C9B06D-8774-4AE7-A6E4-89DF016D0627', 'E0010122-0000-0000-0000-000000000000'),
('86C9B06D-8774-4AE7-A6E4-89DF016D0627', 'E0010123-0000-0000-0000-000000000000'),
('86C9B06D-8774-4AE7-A6E4-89DF016D0627', 'E0010124-0000-0000-0000-000000000000'),
('86C9B06D-8774-4AE7-A6E4-89DF016D0627', 'E0010125-0000-0000-0000-000000000000');