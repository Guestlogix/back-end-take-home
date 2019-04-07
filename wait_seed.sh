echo "Wait while we seed data"
while ! curl -s http://localhost:5000/api/health
do
  docker-compose logs --tail=1 app;
  sleep 15;
done
echo ""
echo "You can now visit http://localhost:5000/apidocs"
