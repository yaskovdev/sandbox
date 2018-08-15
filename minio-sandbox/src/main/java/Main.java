import java.io.ByteArrayInputStream;

import io.minio.MinioClient;
import io.minio.errors.InvalidEndpointException;
import io.minio.errors.InvalidPortException;

public class Main {

	private final MinioClient client;

	private Main() throws InvalidPortException, InvalidEndpointException {
		this.client = new MinioClient("http://127.0.0.1", 9000, "3L244TGIRLWIBE1VEICF", "WF8thABOibNI0RDQE+fmvh6A1Di2G6O9xn2CKjqD");
	}

	public static void main(String[] args) throws InvalidPortException, InvalidEndpointException {
		new Main().process();
	}

	private void process() {
		try {
			final String sourceBucket = "sorting.line.image.performance.testing";
			final String destinationBucket = "sorting.line.image.performance.testing.archive";
//			client.makeBucket(sourceBucket);
//			client.makeBucket(destinationBucket);
			final String objectName = "/2018/08/eb07bff9-cf5d-4bf0-92c8-d0de3addd2a8" + ".govno";
//			client.putObject(sourceBucket, objectName, new ByteArrayInputStream(new byte[] { 1, 2, 3 }), 3, "application/xml");
			client.copyObject(sourceBucket, objectName, destinationBucket);
//			client.removeObject(sourceBucket, objectName);
		} catch (final Exception e) {
			throw new RuntimeException(e);
		}
	}
}
