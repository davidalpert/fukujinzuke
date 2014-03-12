namespace Fukujinzuke.AST

module ASTtoObjectModelVisitor =
    let Visit ast =
        let (FeatureFile f) = ast
        let (FeatureHeader(FeatureTitle(title),FeatureDescription(description))) = f
        let mutable feature = new Fukujinzuke.ObjectModel.Feature()
        feature.Title <- title
        feature.Description <- description
        feature

