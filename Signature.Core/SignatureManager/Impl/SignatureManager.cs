using Common.Database;
using Common.Meta;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;
using Signature.Core.AppleDeveloperManager;
using Signature.Models.AppleDeveloper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace Signature.Core.SignatureManager.Impl
{
    public class SignatureManager
    {
        public Database Db { get; set; }
		public IAppleDeveloperManager AppleDeveloperManager { get; set; }

		public ReturnValue<bool> SetUDID(string udid)
        {
            Db.Connection(conn =>
            {

            });

            return new ReturnValue<bool>();
        }
		


		private X509Certificate2 GetSigningCertificate(string subject)
		{
			X509Certificate2 theCert = null;
			foreach (StoreName name in Enum.GetValues(typeof(StoreName)))
			{
				foreach (StoreLocation location in Enum.GetValues(typeof(StoreLocation)))
				{
					var store = new X509Store(name, location);
					store.Open(OpenFlags.ReadOnly);
					foreach (X509Certificate2 cert in store.Certificates)
					{
						if (cert.Subject.ToLower().Contains(subject.ToLower()) && cert.HasPrivateKey)
						{
							theCert = cert;
							break;
						}
					}
					store.Close();
				}
			}
			if (theCert == null)
			{
				throw new Exception(
					String.Format("No certificate found containing a subject '{0}'.",
								  subject));
			}

			return theCert;
		}

		private byte[] EncodeAndSign(string input)
		{
			var _signingCertificate = GetSigningCertificate(string.Empty);
			AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(_signingCertificate.PrivateKey);
			X509Certificate bouncy = new X509CertificateParser().ReadCertificate(_signingCertificate.GetRawCertData());
			byte[] content = new UTF8Encoding().GetBytes(input);
			var signedDataGenerator = new CmsSignedDataGenerator();
			var processableByteArray = new CmsProcessableByteArray(content);

			IList certCollection = new ArrayList();
			var chain = new X509Chain();
			chain.Build(_signingCertificate);
			foreach (X509ChainElement link in chain.ChainElements)
			{
				certCollection.Add(DotNetUtilities.FromX509Certificate(link.Certificate));
			}
			IX509Store certStore = X509StoreFactory.Create("Certificate/Collection",
														   new X509CollectionStoreParameters(
															   certCollection));

			signedDataGenerator.AddCertificates(certStore);
			signedDataGenerator.AddSigner(keyPair.Private, bouncy, CmsSignedGenerator.DigestSha1);

			CmsSignedData signedData = signedDataGenerator.Generate(processableByteArray, true);
			return signedData.GetEncoded();
		}

	}
}
